using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsShopBehavior : MonoBehaviour
{
    private bool opened;
    float position;
    public Button openShop;
    private Vector3 startPosition;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        opened = false;
        openShop.onClick.AddListener(OpenOverTime);
        startPosition = this.transform.position;
    }




    public void OpenOverTime()
    {

        newPos = new Vector3(startPosition.x, startPosition.y + 4f);
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