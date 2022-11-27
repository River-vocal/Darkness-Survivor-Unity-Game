using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKey : MonoBehaviour
{
    // [SerializeField] private float boostValue = 50f;
    [SerializeField] private GameObject obstacle;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            // Energy energy = other.gameObject.GetComponent<Energy>();
            // energy.CurEnergy += boostValue;
            // GlobalAnalysis.healing_energy += boostValue;
            
            if (obstacle != null)
            {
                obstacle.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}