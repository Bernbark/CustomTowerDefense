using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private Transform timerHolder;
    [SerializeField] private Button button;
    bool timerExists = false;

    private string tooltip;
    public GameObject spawnWave;
    
    public enum SpawnState { SPAWNING, WAITING, COUNTING, START };
    [System.Serializable]
    public  class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float searchCountdown = 1f;

    private SpawnState state = SpawnState.START;
    // Start is called before the first frame update
    void Start()
    {
        tooltip = "Spawn Wave";
        Button spawnWaveButton = spawnWave.GetComponent<Button>();
        spawnWaveButton.onClick.AddListener(SpawnWaveOnClick);
        button.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(tooltip);
        button.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if(state == SpawnState.WAITING)
        {
            
            
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                
                return;
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0f)
        {
            if(state != SpawnState.SPAWNING)
            {
                // Start spawning
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            if(state != SpawnState.COUNTING)
            {
                StartCoroutine(TimerStart());
            }         
            waveCountdown -= Time.deltaTime;    
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
            searchCountdown = 1f;
        }
        return true;
    }

    IEnumerator TimerStart()
    {
        yield return new WaitForSeconds(.01f);
        state = SpawnState.COUNTING;
        for (int i = 5; i > 0; i--)
        {
            string temp = (i).ToString();
            TimerPopUp.Create(timerHolder.position, temp);
            yield return new WaitForSeconds(1);
        }
        
        yield break;
    }

    public void SpawnWaveOnClick()
    {
        if(state != SpawnState.COUNTING && state != SpawnState.WAITING)
        {
            StartCoroutine(SpawnWave(waves[nextWave]));
        }
        
        
        
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = -1;

        }
        nextWave++;
        tooltip = "Next Wave = Wave " + nextWave;
        state = SpawnState.SPAWNING;

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            if(i % 2 == 0)
            {
                SpawnEnemy(_wave.enemies[1]);
            }
            else
            {
                SpawnEnemy(_wave.enemies[0]);
            }
            
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        
        Instantiate(_enemy, transform.position, Quaternion.identity);
    }

    void WaveCompleted()
    {
        state = SpawnState.START;
        waveCountdown = timeBetweenWaves;
        // Start counting down to next wave, tell player wave is over
        
        
    }
}
