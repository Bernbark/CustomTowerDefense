using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject bottomCam;
    public bool home = true;
    
    public Button moveDown;
    public Button moveUp;
    
    // Start is called before the first frame update
    void Start()
    {    
        moveDown.onClick.AddListener(Move);
        moveUp.onClick.AddListener(Move);
    }   

    private void Move()
    {
        if (home)
        {
            mainCam.SetActive(false);
            bottomCam.SetActive(true);
            home = false;
        }
        else
        { 
            bottomCam.SetActive(false);
            mainCam.SetActive(true);
            home = true;
        }
        
    }

    
}
