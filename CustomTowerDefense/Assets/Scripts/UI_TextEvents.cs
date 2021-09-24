
using UnityEngine;
using System;
using TMPro;


public class UI_TextEvents : MonoBehaviour
{

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killsText, maxXPText, currentXPText, buildCostText;
    public event EventHandler OnGoldEarned;
    public Player player;
    public BuildingManager build;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(timer > .1f || Time.timeScale == 0)
        {
            UpdateStats();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
    }

    public void UpdateStats()
    {
        goldText.text = "Gold: " + player.GetGold().ToString();
        levelText.text = "Level: " + player.GetLevel().ToString();
        killsText.text = "Kills: " + player.GetKills().ToString();
        maxXPText.text = "XP To Level: " + player.GetXPToLevel().ToString();
        currentXPText.text = "Current XP: " + player.GetCurrentXP().ToString();
        buildCostText.text = "Build Cost: " + build.GetCost().ToString();
    }
}
