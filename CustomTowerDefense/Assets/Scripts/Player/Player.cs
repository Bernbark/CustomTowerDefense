using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float saveTimer = 0;
    public float statsUpdateCD = 0;
    public int Gold = 20;
    public int Level;
    public int XPToLevel;
    public int MaxXP = 10;
    public int Kills = 0;


    public BuildingManager buildingManager;
    public UI_TextEvents textEvents;
    [SerializeField] private BuildingTypeSO activeBuildingType;
    private void Awake()
    {
        SaveGameManager.Instance.Load();
        LoadPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        int curGold = this.Gold;
        
        saveTimer += Time.deltaTime;
        if(saveTimer >= 5f)
        {
            SavePlayer();
            
            saveTimer = 0;
        }
        
    }

    void ChangeText()
    {

    }

    public void SubtractGold(int gold)
    {
        
        if(this.Gold >= gold)
        {
            this.Gold -= gold;
        }
        else
        {
            this.Gold = 0;
        }
        
    }

    // Prevent player from permanently being unable to build due to leaks.
    public void SubtractGoldFromLeak(int gold)
    {
        
        if (this.Gold  >= gold+(Kills * 10))
        {
            this.Gold -= gold;
        }
    }

    public void LevelUp()
    {
        XPToLevel -= MaxXP;
        this.Level += 1;
        MaxXP *= 10;
    }

    public void AddXP(int xp)
    {
        this.XPToLevel += xp;
        if (XPToLevel >= MaxXP)
        {          
            LevelUp();
        }
    }

    public void AddGold(int gold)
    {
        this.Gold += gold;
    }

    public int GetGold()
    {
        return this.Gold;
    }

    public int GetLevel()
    {
        return this.Level;
    }

    public int GetCurrentXP()
    {
        return XPToLevel;
    }

    public int GetXPToLevel()
    {
        return MaxXP;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        SaveGameManager.Instance.Save();
    }

    public void LoadPlayer()
    {
        
        PlayerData data = SaveSystem.LoadPlayer();
        
        if (data != null)
        {                    
            Level = data.Level;
            Gold = data.Gold;
            MaxXP = data.MaxXP;
            XPToLevel = data.XPToLevel;
            Kills = data.Kills;
        }
        else
        {
            SetDefaultStats();
        }
        
    }

    public void SetLevel(int level)
    {
        this.Level = level;
    }

    public void SetGold(int gold)
    {
        this.Gold = gold;
    }

    public void SetMaxXP(int maxXP)
    {
        this.MaxXP = maxXP;
    }

    public void SetCurrentXP(int currentXP)
    {
        this.XPToLevel = currentXP;
    }

    public int GetKills()
    {
        return this.Kills;
    }

    public void SetDefaultStats()
    {
        this.Level = 0;
        this.Gold = 20;
        this.MaxXP = 10;
        this.XPToLevel = 0;
        this.Kills = 0;
        
    }

    public void AddToKillCount()
    {
        this.Kills++;
    }

    public void WipeData()
    {
        SetDefaultStats();
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            Destroy(turret);
        }
        SaveGameManager.Instance.SaveableObjects.Clear();
        
        buildingManager.SetCost(SaveGameManager.Instance.SaveableObjects.Count);
        AstarPath.active.Scan();
    }
}
