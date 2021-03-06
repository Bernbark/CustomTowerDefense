using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodShopBehavior : MonoBehaviour
{
    // Unlock new tower types

    // Make enemies worth more kills

    // Make enemies worth more gold

    // Special abilities, speed up towers, slow down enemies, damage the whole screen

    public Player player;
    private bool opened;
    float position;
    public Button openShop, buyKillsPerDeathButton;
    private Vector3 startPosition;
    Vector3 newPos;
    
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        opened = false;
        openShop.onClick.AddListener(OpenOverTime);
        buyKillsPerDeathButton.onClick.AddListener(IncrementBlood);
        startPosition = this.transform.position;
    }



    private void IncrementBlood()
    {
        if (player.GetBlood() >= BloodShopData.killsModCost)
        {
            player.SubtractBlood((int)BloodShopData.killsModCost);
            BloodShopData.IncrementValueMod();

        }
    }

    public void OpenOverTime()
    {

        newPos = new Vector3(startPosition.x - 6f, startPosition.y);
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
}
