using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SaveGameManager : MonoBehaviour
{
    private static SaveGameManager instance;

    public List<SaveableObject> SaveableObjects { get; private set; }

    public static SaveGameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveGameManager>();
            }
            return instance;
        }
    }

    Snap snappingTool;

    private void Awake()
    {
        SaveableObjects = new List<SaveableObject>();
        snappingTool = GetComponent<Snap>();
        
    }

    public void Save()
    {
        // save by level
        PlayerPrefs.SetInt(Application.loadedLevel.ToString(), SaveableObjects.Count);
        for(int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }

    public void Load()
    {
        foreach(SaveableObject obj in SaveableObjects)
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        SaveableObjects.Clear();

        int objectCount = PlayerPrefs.GetInt(Application.loadedLevel.ToString());

        for(int i = 0; i < objectCount; i++)
        {
            GameObject tmp = null;
            //index 0 is objectType, index 1 is the position, 2 is range, 3 is damage
            List<string> value = new List<string>(PlayerPrefs.GetString(Application.loadedLevel+"-"+ i.ToString()).Split('_'));


            switch (value[0])
            {
                case "SquareTurret":
                    tmp = Instantiate(Resources.Load("BuildingBase1") as GameObject);
                    

                    break;
                case "CircleTurret":
                    tmp = Instantiate(Resources.Load("Circle") as GameObject);
                    break;

                case "ShotgunTurret":
                    tmp = Instantiate(Resources.Load("ShotgunTurret") as GameObject);
                    

                    break;
            }
            if(tmp!= null)
            {
                
                tmp.GetComponent<SaveableObject>().Load(value);
                
            }

            
        }


        AstarPath.active.Scan();
    }
    

    public Vector3 StringToVector(string value)
    {
        //(1, 2, 3)
        value = value.Trim(new char[] { '(', ')' });
        //1, 2, 3
        value = value.Replace(" ", "");
        //1,2,3
        string[] pos = value.Split(',');

        return new Vector3(Mathf.RoundToInt(float.Parse(pos[0])/.25f)*.25f, Mathf.RoundToInt(float.Parse(pos[1]) / .25f) * .25f, Mathf.RoundToInt(float.Parse(pos[2]) / .25f) * .25f);
    }

    public Quaternion StringToQuaternion(string value)
    {
        return Quaternion.identity;
    }
}
