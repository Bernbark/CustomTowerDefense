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

    public void WipePlayerData(Button button)
    {
        player.SetDefaultStats();
        List<GameObject> turrets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Shotgun"));
        GameObject[] others = GameObject.FindGameObjectsWithTag("MachineGun");
        foreach(GameObject obj in others)
        {
            turrets.Add(obj);
        }
        
        foreach(GameObject turret in turrets)
        {
            Destroy(turret);
            
        }
        turrets.Clear();
        SaveGameManager.Instance.SaveableObjects.Clear();
        SaveGameOnClick();
        buildingManager.SetCost(SaveGameManager.Instance.SaveableObjects.Count);
        AstarPath.active.Scan();
        Debug.Log(SaveGameManager.Instance.SaveableObjects.Count);
        KillsShopData.valueMod = 1;
        KillsShopData.valueCost = 100;
        KillsShopData.UpdateText_Static();
    }

                    
}
