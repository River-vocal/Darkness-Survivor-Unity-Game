using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectSystemManager : MonoBehaviour
{
    public GameObject BleedParticleEffect;
    public GameObject playerBulletPrefab;
    public GameObject HitSparklingEffect;
    
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
}
