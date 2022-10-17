using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLightController : MonoBehaviour
{
    public Light2D spotLight;
    // 1: healing light, 0: neutral light, -1: damaging Light
    private int curStatus;
    private readonly int UNDER_HEALING_LIGHT = 1;
    private readonly int UNDER_NEUTRAL_LIGHT = 0;
    private readonly int UNDER_DAMAGING_LIGHT = -1;
    private readonly int NO_LIGHT = 2;

    private readonly float BASIC_STATUS = 0.2f;
    private readonly float BOOST_STATUS = 0.8f;

    private readonly float INTENSITY_UNDER_DARKNESS = 1.2f;

    private float originalIntensity;
    private bool isInDarkLevel;
    
    void Start()
    {
        // player = GetComponent<PlayerControllerNew>();
        spotLight.color = Color.white;
        spotLight.intensity = BASIC_STATUS;
        curStatus = NO_LIGHT;
        isInDarkLevel = SceneManager.GetActiveScene().name.Equals("DarkLevel1");
        // SceneManager.GetActiveScene().buildIndex
        if (isInDarkLevel)
        {
            spotLight.intensity = 0.4f;
            spotLight.pointLightOuterRadius = 6.5f;
            spotLight.pointLightInnerRadius = 1.5f;
        }

        originalIntensity = spotLight.intensity;
    }
    
    void Update()
    {
        // spotLight.color = new Color(0.45f + 4f, 1, 0.5f + 5f, 0.3f + 5f);
        
        
        // player.attackDamage = Math.Max((int)Math.Round(volumeIntensity), minDamage) ;
    }
    

    public void EnterHealingLight()
    {
        curStatus = UNDER_HEALING_LIGHT;
        spotLight.color = Color.green;
        spotLight.intensity = BOOST_STATUS + 0.5f;
        EnterHelperInDark();
    }

    public void EnterNeutralLight()
    {
        curStatus = UNDER_NEUTRAL_LIGHT;
        spotLight.color = Color.white;
        spotLight.intensity = BOOST_STATUS;
        EnterHelperInDark();
    }

    public void EnterDamagingLight()
    {
        curStatus = UNDER_DAMAGING_LIGHT;
        spotLight.color = Color.red;
        spotLight.intensity = BOOST_STATUS + 0.2f;
        EnterHelperInDark();
    }

    private void EnterHelperInDark()
    {
        if (isInDarkLevel)
        {
            spotLight.pointLightOuterRadius += 3f;
        }
    }

    public void LeaveLight()
    {
        curStatus = NO_LIGHT;
        spotLight.color = Color.white;
        spotLight.intensity = originalIntensity;
        if (isInDarkLevel)
        {
            spotLight.pointLightOuterRadius -= 3f;
        }
    }
}
