using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBeats : AudioEffector
{
    public Light2D spotLight;
    public SwordAttack swordAttack;
    public AudioSource audioSource;
    // private float time = 3f;
    public float effectorMultiplier = 200f;

    private int minDamage = 10;


    private double volumeIntensity;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
    
    public override void Update()
    {
        base.Update();
                    
        volumeIntensity = effectorValue * effectorMultiplier;
        // Debug.Log(effectorValue + ",  " + effectorMultiplier + ";  " + volumeIntensity);
        spotLight.color = new Color(0.45f + effectorValue * 4f, 1, 0.5f + effectorValue * 5f, 0.3f + effectorValue * 5f);

        // Debug.Log(volumeIntensity * 0.5f + ",  " + volumeIntensity * 1f + ";  " + volumeIntensity * 1.5f);
        /*Debug.Log(effectorValue);
        if (volumeIntensity > 30f)
        {
            spotLight.color = Color.red;
        }
        else if (volumeIntensity > 10f)
        {
            spotLight.color = Color.blue;
        }
        else
        {
            spotLight.color = Color.white;
        }*/
        swordAttack.damage = Math.Max((int)Math.Round(volumeIntensity), minDamage) ;
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        time -= Time.fixedDeltaTime;
        if (time <= 0) {
            if (spotLight.color == Color.white) {
                spotLight.color = Color.red;
                time = 1.0f;
                swordAttack.damage = 40;
            }
            else {
                spotLight.color = Color.white;
                time = 3f;
                swordAttack.damage = 10;
            }
        }
    }*/
}
