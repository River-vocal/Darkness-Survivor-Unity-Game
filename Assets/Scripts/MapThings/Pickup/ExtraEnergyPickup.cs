using System;
using UnityEngine;

public class ExtraEnergyPickup : MonoBehaviour
{
    public const string DefaultID = "NOT SET";

    // ID is needed in level loader(save and load)
    // Set ID in unity editor to let player collect only once
    [SerializeField] public string id = DefaultID;
    [SerializeField] private StringEventChannel loadEventChannel;
    [SerializeField] private FloatEventChannel enlargeEnergyBarEventChannel;
    [SerializeField] private StringEventChannel saveEventChannel;
    [SerializeField] private float boostValue = 30f;

    private void OnEnable()
    {
        loadEventChannel.AddListener(Collected);
    }

    private void OnDisable()
    {
        loadEventChannel.RemoveListener(Collected);
    }

    private void Collected(string pickupId)
    {
        if (pickupId != id) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Energy energy = other.gameObject.GetComponent<Energy>();
            energy.MaxEnergy += boostValue;
            energy.CurEnergy += boostValue;
            enlargeEnergyBarEventChannel.Broadcast(boostValue);
            saveEventChannel.Broadcast(id);
            Destroy(gameObject);
        }
    }
}