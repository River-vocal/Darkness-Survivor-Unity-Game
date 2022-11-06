using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private VoidEventChannel playerReachExitEventChannel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        playerReachExitEventChannel.Broadcast();
    }
}