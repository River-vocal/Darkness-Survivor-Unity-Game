using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psAttackManager : MonoBehaviour
{
    public Player Player;
    private ParticleSystem ps;
    public HashSet<Collider2D> set;
    
    // Start is called before the first frame update
    void Start()
    {
        set = new HashSet<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        
        //add all little enemy colliders in the scene to ps trigger
        var all = GameObject.FindGameObjectsWithTag("LittleEnemy");
        int index = 0;
        foreach (var colliderObject in all)
        {
            var col = colliderObject.GetComponent<Collider2D>();
            if (col == null)
            {
                Debug.LogError("Could not find a collider component on one of the little enemy objects!");
            }
            else
            {
                ps.trigger.SetCollider(index++, col);
            }
        }

        all = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var colliderObject in all)
        {
            var col = colliderObject.GetComponent<Collider2D>();
            if (col == null)
            {
                Debug.LogError("Could not find a collider component on one of the little enemy objects!");
            }
            else
            {
                ps.trigger.SetCollider(index++, col);
            }
        }
    }

    void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        
        //get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var insideData);
        
        for (int i = 0; i < numEnter; ++i)
        {
            var other = insideData.GetCollider(i, 0).GetComponent<Collider2D>();
            if (!set.Contains(other))
            {
                Player.DealDamageTo(other);
                set.Add(other);
            }
        }
    }
}
