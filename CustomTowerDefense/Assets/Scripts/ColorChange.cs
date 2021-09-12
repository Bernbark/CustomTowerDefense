using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChange : MonoBehaviour
{
    bool validPosition;
    float timeToChange;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        validPosition = false;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (validPosition)
        {
            // As long as SpriteRenderer exists
            if (spriteRenderer != null)
            {
                Color newColor = Color.green;
                spriteRenderer.color = newColor;
            }
            
        }
        else
        {
            if (spriteRenderer != null)
            {
                Color newColor = Color.red;
                newColor.a = 0;
                        
                spriteRenderer.color = newColor;
            }
            
        }
    }

    public void SetIsValidPosition(bool validPosition)
    {
        this.validPosition = validPosition;
    }
}
