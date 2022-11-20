using System;
using UnityEngine;

public class DisableReceiver : MonoBehaviour
{
    public VoidEventChannel disableChannel;

    private void OnEnable()
    {
        disableChannel.AddListener(DisableSelf);
    }

    private void OnDisable()
    {
        disableChannel.RemoveListener(DisableSelf);
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}