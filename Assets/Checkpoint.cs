using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public Sprite checkedSprite;
    private bool reached = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (reached) return;
            Respawn respawn = col.GetComponent<Respawn>();
            respawn.respawnLocation = transform.position;

            Image image = gameObject.GetComponentInChildren<Image>();
            image.sprite = checkedSprite;

            reached = true;
        }
    }
}
