using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public event EventHandler OnPlayerReachExit;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Play reach finish point");
        if(OnPlayerReachExit!=null) OnPlayerReachExit(this, EventArgs.Empty);
    }
}
