using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button openShop, addValue;
    
    public Player player;
    private Vector3 startPosition;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        opened = false;
        openShop.onClick.AddListener(OpenOverTime);
        addValue.onClick.AddListener(IncrementValue);
        
        startPosition = this.transform.position;
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
}