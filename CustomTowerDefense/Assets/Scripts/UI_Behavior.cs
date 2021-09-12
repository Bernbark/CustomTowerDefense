using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Behavior : MonoBehaviour
{
    private bool hidden = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (hidden)
            {
                Show();
                hidden = false;
            }
            else
            {
                Hide();
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
