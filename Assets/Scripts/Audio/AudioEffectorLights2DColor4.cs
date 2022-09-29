using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class AudioEffectorLights2DColor4 : AudioEffector
{
    public float effectorMultiplier = 1;

    public float minLightIntensity = 0.2f;

    private Light2D light2D;

    private float lightIntensity;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        lightIntensity = effectorValue * effectorMultiplier * 3f;
        // Debug.Log(lightIntensity);
        if (lightIntensity < minLightIntensity)
        {
            lightIntensity = minLightIntensity;
        }

        light2D.intensity = Math.Min(0.21f, lightIntensity);
        light2D.color = new Color(0.4f + effectorValue * 5f, 1, 0.8f - effectorValue * 5f, 0.2f + effectorValue * 3f);
    }
}