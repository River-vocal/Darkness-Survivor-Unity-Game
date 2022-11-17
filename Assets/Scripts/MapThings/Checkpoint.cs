using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    private bool reached = false;
    [SerializeField] public GameObject preCheckParticleEffect;
    [SerializeField] public GameObject postCheckParticleEffect;
    private static Checkpoint _curCheckpoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (reached) return;
            Respawn respawn = col.GetComponent<Respawn>();
            respawn.respawnLocation = transform.position;

            if (_curCheckpoint)
            {
                _curCheckpoint.preCheckParticleEffect.SetActive(true);
                _curCheckpoint.postCheckParticleEffect.SetActive(false);
            }

            preCheckParticleEffect.SetActive(false);
            postCheckParticleEffect.SetActive(true);

            reached = true;
        }
    }
}