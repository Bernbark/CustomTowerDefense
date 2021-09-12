using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedTransform
{
    public float[] position = new float[3];
    public float[] scale = new float[3];


    public SerializedTransform(Transform transform)
    {
        if(transform != null)
        {
            position[0] = transform.localPosition.x;
            position[1] = transform.localPosition.y;
            position[2] = transform.localPosition.z;

            scale[0] = transform.localScale.x;
            scale[1] = transform.localScale.y;
            scale[2] = transform.localScale.z;
        }
        
    }

    public float[] getPosition()
    {
        return position;
    }

    public float[] getScale()
    {
        return scale;
    }
}

