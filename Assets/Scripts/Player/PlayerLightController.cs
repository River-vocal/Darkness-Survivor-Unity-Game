using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public Light2D spotLight;
    public PlayerControllerNew player;
    // private float time = 3f;
    public float effectorMultiplier = 200f;

    private int minDamage = 10;


    private double volumeIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControllerNew>();
    }
    
    void Update()
    {
        // Debug.Log(effectorValue + ",  " + effectorMultiplier + ";  " + volumeIntensity);
        spotLight.color = new Color(0.45f + 4f, 1, 0.5f + 5f, 0.3f + 5f);

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
        player.attackDamage = Math.Max((int)Math.Round(volumeIntensity), minDamage) ;
    }
}
