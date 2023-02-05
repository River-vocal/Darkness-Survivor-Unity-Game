using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{

    private int counter = 30;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter <= 0)
        {
            
            Debug.Log("Destroy hitEffect");
            
            Destroy(gameObject);
        }
        else
        {
            counter--;
        }
    }
}
