
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OfflineProgress : MonoBehaviour
{
    private bool hidden = false;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI instructions;
    public Button close;
    public Player player;
    float goldToEarn;
    void Start()
    {
        Time.timeScale = 0;
        close.onClick.AddListener(Hide);
        if (PlayerPrefs.HasKey("LAST_LOGIN"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));
            TimeSpan span = DateTime.Now - lastLogin;
            goldToEarn = (float)span.TotalMinutes * player.GetKills() * KillsShopData.valueMod;
            player.AddGold((int)goldToEarn);
            goldText.text = "Gold Earned Offline: " + goldToEarn;
            timeText.text = string.Format("You were gone for: {0} Days {1} Hours {2} Minutes {3} Seconds",span.Days,span.Hours,span.Minutes,span.Seconds);
        }
        else
        {
            instructions.text = "Welcome to Custom Tower Defense. Prepare to defend yourself by holding Shift to build towers. Press Tab to see building options and stats and close menus";
        }
    }

    private void Update()
    {
        if (!hidden)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Hide();
                hidden = true;
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LAST_LOGIN", DateTime.Now.ToString());
    }


    private void Hide()
    {
        //Debug.Log("Should be hidden");
        transform.localScale = new Vector3(0f, 0f, 0f);
        Time.timeScale = 1;
        
    }

}
