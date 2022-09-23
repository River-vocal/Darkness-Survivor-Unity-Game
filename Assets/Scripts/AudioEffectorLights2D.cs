using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class AudioEffectorLights2D : AudioEffector
{
    public float effectorMultiplier = 1;

    public float minLightIntensity = 1;

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
        lightIntensity = effectorValue * effectorMultiplier;

        if (lightIntensity < minLightIntensity)
        {
            lightIntensity = minLightIntensity;
        }

        light2D.intensity = lightIntensity;
    }
}
