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
    public BuildingManager buildingManager;
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
        SaveSystem.SavePlayer(player);
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
        SaveSystem.LoadPlayer();
        //var graphToScan = AstarPath.active.data.gridGraph;
        //AstarPath.active.Scan(graphToScan);
        
        
    }

    public void WipePlayerData(Button button)
    {
        player.SetDefaultStats();
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach(GameObject turret in turrets)
        {
            Destroy(turret);
        }
        SaveGameManager.Instance.SaveableObjects.Clear();
        SaveGameOnClick();
        buildingManager.SetCost(SaveGameManager.Instance.SaveableObjects.Count);
        AstarPath.active.Scan();
        Debug.Log(SaveGameManager.Instance.SaveableObjects.Count);
        textEvents.UpdateStats();
    }

                    
}
