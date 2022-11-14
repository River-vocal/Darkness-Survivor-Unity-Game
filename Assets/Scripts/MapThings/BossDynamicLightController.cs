using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDynamicLightController : MapLightController
{
    [SerializeField] public float lightSwitchIntervalTime = 3f;
    [SerializeField][Range(-1,1)] private int initialType;
    private float lightIntensityDecreasePerSecond;
    protected float originalIntensity;
    [SerializeField] protected PlatformDisappearController pdc;

    private int curTypeIdx;
    // 0, 1, 2 red white green
    
    // Start is called before the first frame update
    protected override void Start()
    {
        typeOfLight = initialType;
        curTypeIdx = initialType + 1;
        base.Start();
        if (SceneManager.GetActiveScene().name.Equals("DarkLevel1"))
        {
            curLight.intensity = 1.0f;
        }
        originalIntensity = curLight.intensity;
        lightIntensityDecreasePerSecond = originalIntensity * 0.95f / lightSwitchIntervalTime;
        IEnumerator coroutine = UpdateColor();
        StartCoroutine(coroutine);
    }

    protected void Update()
    {
        if (lightSwitchIntervalTime > 1f)
        {
            curLight.intensity -= lightIntensityDecreasePerSecond * Time.deltaTime;
        }
    }

    IEnumerator UpdateColor()
    {
        if (pdc != null)
        {
            yield return new WaitForSeconds(pdc.startTime);
        }
        while (true)
        {
            yield return new WaitForSeconds(lightSwitchIntervalTime);
            if (playerIsIn)
            {
                playerEnergy.ConsumeSpeed += tmpSpeed * typeOfLight;
                if (tmpSpeed == 0)
                {
                    playerEnergy.ConsumeSpeed += originalConsumeSpeed;
                }
                playerLightController.LeaveLight();
            }
            curTypeIdx = (curTypeIdx + 1) % 2;
            typeOfLight = curTypeIdx - 1;
            if (playerIsIn)
            {
                OnTriggerEnterHelper(playerEnergy, playerLightController);
            }

            if (typeOfLight == DAMAGING_LIGHT)
            {
                bossAnimator.SetBool("IsEnraged", true);
            } else if (typeOfLight == NEUTRAL_LIGHT || typeOfLight == HEALING_LIGHT)
            {
                bossAnimator.SetBool("IsEnraged", false);
            }

            SetLightColor();
            curLight.intensity = originalIntensity;
        }
    }
}
