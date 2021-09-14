using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerPopUp : MonoBehaviour
{
    
    
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    // Start is called before the first frame update
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 7f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public static TimerPopUp Create(Vector3 positon, string time)
    {
        Transform timerPopup = Instantiate(GameAssets.i.pfTimerPopup, positon, Quaternion.identity);
        
        TimerPopUp timer = timerPopup.GetComponent<TimerPopUp>();
        timer.Setup(time);
        
        return timer;
    }
    
    public void Setup(string time)
    {
        disappearTimer = .35f;
        textMesh.text = time.ToString();
        textColor = textMesh.color;
    }
}
