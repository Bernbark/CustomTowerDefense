using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class ButtonHandlin : MonoBehaviour
{
    //public AstarPath path;
    public UI_TextEvents textEvents;
    public Player player;
    public EnemyBehavior enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        //if file gets corrupted because i'm stupid
        //WipePlayerData(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainGoldOnClick(Button button)
    {
        player.AddGold(1);        
    }

    /**
     * 
     *                  Saving / Loading / Wiping Data
     * 
     */
    public void SaveGameOnClick()
    {
        SaveGameManager.Instance.Save();
    }
    
    public void LoadGameOnClick(Button button)
    {
        /**
        PlayerData data = SaveSystem.LoadPlayer();

        player.SetLevel(data.Level);
        player.SetGold(data.Gold);
        player.SetMaxXP(data.MaxXP);
        player.SetCurrentXP(data.XPToLevel);
        */
        SaveGameManager.Instance.Load();
        //var graphToScan = AstarPath.active.data.gridGraph;
        //AstarPath.active.Scan(graphToScan);
        
        
    }

    public void WipePlayerData(Button button)
    {
        player.SetDefaultStats();
        
        SaveGameOnClick();

        
    }

                    
}
