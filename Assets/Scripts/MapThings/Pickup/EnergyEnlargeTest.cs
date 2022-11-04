using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEnlargeTest : MonoBehaviour
{
    private float boostValue = 10f;
    private EnergyBarResizer energyBar;

    private void Awake() {
        energyBar = GameObject.Find("EnergyBar").GetComponent<EnergyBarResizer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Energy energy = other.gameObject.GetComponent<Energy>();
            energy.MaxEnergy+=boostValue;
            energy.CurEnergy+=boostValue;
            energyBar.Enlarge(50);
            Destroy(transform.parent.gameObject);
        }
    }
}
