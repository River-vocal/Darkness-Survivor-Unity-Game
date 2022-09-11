using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalTimer : MonoBehaviour
{
    public static int millSec;
    private int state = 0;  // 0: before input enable, 1: before input disable, 2: before game update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        millSec = (int) ((Time.fixedTime * 1000) % 1000);
        switch (state) {
            case 0:
                if (millSec > 900) {
                    state = 1;
                }
                break;
            case 1:
                if (millSec > 100) {
                    state = 2;
                }
                break;
            case 2:
                if (millSec > 500) {
                    state = 0;
                }
                break;
        }
    }
}