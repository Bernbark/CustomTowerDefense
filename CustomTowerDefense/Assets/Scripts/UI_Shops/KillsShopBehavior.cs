
using UnityEngine;
using UnityEngine.UI;


public class KillsShopBehavior : MonoBehaviour
{
    // Make enemies stronger/valuable based on kills (increase the amount)
    
    // Make blood generate

    // Make blood generate faster

    // Trade gold for blood (make war? espionage?)

    // Unlock Public Relations

    // Unlock ability for random boss spawn

    // Make it more likely to happen

    private bool opened;
    float position;
    public Button openShop, addValue, bloodPerSecButton;
    
    public Player player;
    private Vector3 startPosition;
    Vector3 newPos;
    private float bloodTimer;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        opened = false;
        openShop.onClick.AddListener(OpenOverTime);
        addValue.onClick.AddListener(IncrementValue);
        bloodPerSecButton.onClick.AddListener(IncrementBloodPerSec);
        startPosition = this.transform.position;
    }

    private void Update()
    {
        bloodTimer += Time.deltaTime;
        if (bloodTimer >= 1)
        {
            player.AddBlood(KillsShopData.bloodPerSecond);
            BloodShopData.UpdateText_Static();
            bloodTimer = 0;
        }
    }


    public void OpenOverTime()
    {
        newPos = new Vector3(startPosition.x, startPosition.y + 8f);
        if (!opened)
        {
            transform.position = Vector3.Lerp(startPosition, newPos, 1f);
            opened = true;
        }
        else
        {
            transform.position = Vector3.Lerp(this.transform.position, startPosition, 1f);
            opened = false;
        }
    }

    private void IncrementValue()
    {
        if(player.GetKills() >= KillsShopData.valueCost)
        {
            player.SubtractKills(KillsShopData.valueCost);
            KillsShopData.IncrementValueMod();            
        } 
    }

    private void IncrementBloodPerSec()
    {
        if(player.GetKills() >= KillsShopData.bloodPerSecondCost)
        {
            player.SubtractKills(KillsShopData.bloodPerSecondCost);
            KillsShopData.IncrementBloodPerSec_Static();
        }
        
    }
}