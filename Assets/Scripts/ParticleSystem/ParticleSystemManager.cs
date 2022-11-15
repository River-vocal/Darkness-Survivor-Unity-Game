using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public GameObject BleedParticleEffect;
    
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
}
