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
            collision.gameObject.GetComponent<Energy>().CurEnergy -= 10f;
        }
    }
}
