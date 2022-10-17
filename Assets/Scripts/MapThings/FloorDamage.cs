using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if  (collision.gameObject.GetComponent<Energy>().CurEnergy < 10f) {
                GlobalAnalysis.player_status = "trap_dead";
                Debug.Log("lose by: floor");
            }
            GlobalAnalysis.trap_damage += 10;
            collision.gameObject.GetComponent<Energy>().CurEnergy -= 10f;
        }
    }
}
