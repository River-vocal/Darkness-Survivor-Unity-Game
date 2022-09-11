using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        int millSec = GlobalTimer.millSec;
        bool ableToMove = (millSec<100 || millSec > 900);
        if(ableToMove)
        {            
            // Change the 'color' property of the 'Sprite Renderer'
            sprite.color = new Color (1, 0, 0, 1); 
        }
        else
        {
            sprite.color = new Color (0, 1, 0, 1); 
        }
    }
}
