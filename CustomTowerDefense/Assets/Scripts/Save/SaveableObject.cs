using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectType
{
    SquareTurret, CircleTurret
}

public abstract class SaveableObject : MonoBehaviour
{
    public Snap snappingTool;
    protected string save;
    [SerializeField]
    private ObjectType objectType;
    // Start is called before the first frame update
    private void Start()
    {
        SaveGameManager.Instance.SaveableObjects.Add(this); 
        
    }

    public virtual void Save(int id)
    {
        // 0-10  first number is level index, second is amount of objects
        PlayerPrefs.SetString(Application.loadedLevel+"-"+id.ToString(), objectType+"_"+transform.position.ToString()+"_"+save);
    }

    public virtual void Load(string[] values)
    {
        
        transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);

    }

    /**
     * 
     * MUST ALWAYS BE CALLED WHEN DESTROYING TURRETS OR ANY SAVABLE OBJECT
     * 
     */
    public void DestroySaveable()
    {
        SaveGameManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);
    }

    
}
