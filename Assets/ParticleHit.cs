using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public Player Player;
    private Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnParticleCollision(GameObject other)
    {
        var psManager = other.GetComponent<psAttackManager>();
        if (!psManager.set.Contains(collider))
        {
            psManager.set.Add(collider);
            Player.DealDamageTo(collider);
        }
    }
}
