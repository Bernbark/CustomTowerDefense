
using System.Collections.Generic;
using UnityEngine;

public class SkillsShopBehavior : MonoBehaviour
{
    string save;
    private static SkillsShopBehavior Instance { get; set; }

    private int skillPoints;

    void Start()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("SKILLS_SHOP"))
        {
            // 0 is valueMod, 1 is valueCost
            List<string> values = new List<string>(PlayerPrefs.GetString("SKILLS_SHOP").Split('_'));
            

        }
        else
        {
            
        }
        UpdateText();
        

    }

    private void UpdateText()
    {
        
    }
}
