using UnityEngine;

public class ExtraEnergyPickup : MonoBehaviour
{
    [SerializeField] private FloatEventChannel energyEnlargeEventChannel;
    [SerializeField] private float boostValue = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Energy energy = other.gameObject.GetComponent<Energy>();
            energy.MaxEnergy += boostValue;
            energy.CurEnergy += boostValue;
            energyEnlargeEventChannel.Broadcast(boostValue);
            Destroy(gameObject);
        }
    }
}