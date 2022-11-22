using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectSystemManager : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    
    public GameObject BleedParticleEffect;
    public GameObject HitSparklingEffect;
    public GameObject ExplosionNovaFire;
    public GameObject EvilPurpleExplode;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateBleedParticleEffect(Transform transform)
    {
        Instantiate(BleedParticleEffect, transform.position, transform.rotation);
    }

    public void GenerateHitSparklingEffect(Transform transform)
    {
        Instantiate(HitSparklingEffect, transform.position, transform.rotation);
    }

    public void GenerateExplosionNovaFire(Transform transform)
    {
        Instantiate(ExplosionNovaFire, transform.position, transform.rotation);
    }

    public void GenerateEvilPurpleExplode(Transform transform)
    {
        Instantiate(EvilPurpleExplode, transform.position, transform.rotation);
    }
}
