using System;
using UnityEngine;

public class AutoDisableSender : MonoBehaviour
{
    public VoidEventChannel disableChannel;

    private void OnDisable()
    {
        disableChannel.Broadcast();
    }
}
