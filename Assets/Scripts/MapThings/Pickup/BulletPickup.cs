using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            // Energy energy = other.gameObject.GetComponent<Energy>();
            // energy.CurEnergy += boostValue;

            Player player = other.gameObject.GetComponent<Player>();
            player.playerBulletCount += 1;
            if (TopHintArea.getCurrentString() == "No bullets!")
            {
                TopHintArea.reset();
            }
            // GlobalAnalysis.healing_energy += boostValue;
            Destroy(gameObject);
        }
    }
}
