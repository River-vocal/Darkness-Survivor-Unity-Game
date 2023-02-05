using System;
using UnityEngine;

public class DisableReceiver : MonoBehaviour
{
    public VoidEventChannel disableChannel;
    public bool setActive;

    private void OnEnable()
    {
        disableChannel.AddListener(SetActiveSelf);
    }

    private void OnDisable()
    {
        disableChannel.RemoveListener(SetActiveSelf);
    }

    private void SetActiveSelf()
    {
        gameObject.SetActive(setActive);
    }
}