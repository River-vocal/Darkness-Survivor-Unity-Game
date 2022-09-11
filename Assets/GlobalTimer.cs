using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    public static int millSec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        millSec = (int) ((Time.fixedTime * 1000) % 1000);
    }
}
