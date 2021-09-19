using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Button range = pauseBtn.GetComponent<Button>();
        range.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }
}
