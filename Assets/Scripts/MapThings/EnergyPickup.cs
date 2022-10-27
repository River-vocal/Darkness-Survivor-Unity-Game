using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    private float boostValue = 10f;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Energy energy = other.gameObject.GetComponent<Energy>();
            energy.CurEnergy += boostValue;
            GlobalAnalysis.healing_energy += boostValue;
            Destroy(transform.parent.gameObject);
        }
    }
}
