using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private Transform timerHolder;
    bool timerExists = false;

    public GameObject spawnWave;
    
    public enum SpawnState { SPAWNING, WAITING, COUNTING, START };
    [System.Serializable]
    public  class Wave
    {
        public string name;
        public Transform enemy;
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
        Button spawnWaveButton = spawnWave.GetComponent<Button>();
        spawnWaveButton.onClick.AddListener(SpawnWaveOnClick);
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
        if(state != SpawnState.COUNTING)
        StartCoroutine(SpawnWave(waves[nextWave]));
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        
        state = SpawnState.SPAWNING;

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
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
        
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = -1;
            
        }
        nextWave++;
    }
}
