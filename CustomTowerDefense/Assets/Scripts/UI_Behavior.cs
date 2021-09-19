using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Behavior : MonoBehaviour
{
    private bool hidden = true;
    private void Start()
    {
        Hide();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (hidden)
            {
                Show();
                Time.timeScale = 0;
                hidden = false;
            }
            else
            {
                Hide();
                Time.timeScale = 1;
                hidden = true;
            }
        }
    }

    private void Show()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Hide()
    {
        Debug.Log("Should be hidden");
        transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
