using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHit : MonoBehaviour
{
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    
    void ChangeColor()
    {
        objectRenderer.material.SetColor("_Color", Random.ColorHSV(0,1,0.6f,0.8f,0.2f,0.8f,0,1));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ChangeColor();
        }
    }
}
