using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    [SerializeField] public float effectTime = 15f;

    [SerializeField] public float speedFactor = 1.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        var changer = col.gameObject.GetComponent<SpeedChanger>();
        changer.SpeedUp(speedFactor, effectTime);
        Destroy(gameObject);
    }
}