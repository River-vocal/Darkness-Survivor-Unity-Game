using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireDamageToEnergy = 10f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Energy energy = other.gameObject.GetComponent<Energy>();
            other.gameObject.GetComponent<Player>().TakeDamage(fireDamageToEnergy);
        }
    }
}
