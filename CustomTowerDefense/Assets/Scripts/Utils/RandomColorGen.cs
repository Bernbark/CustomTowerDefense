using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorGen : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float colorCD;
    private void Start()
    {
        colorCD = .2f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(colorCD >= .2f)
        {
            Color color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
            spriteRenderer.color = color;
            colorCD = 0;
        }
        else
        {
            colorCD += Time.deltaTime;
        }
    }
}
