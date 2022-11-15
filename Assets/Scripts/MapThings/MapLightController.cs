using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class MapLightController : MonoBehaviour
{
    [Header("Light type Setting")] 
    [SerializeField][Range(-1,1)] protected int typeOfLight;
    // 1: healing light, 0: neutral light, -1: damaging Light

    protected Light2D curLight;
    protected readonly int HEALING_LIGHT = 1;
    protected readonly int NEUTRAL_LIGHT = 0;
    protected readonly int DAMAGING_LIGHT = -1;
    protected float originalConsumeSpeed = 1f;
    protected bool playerIsIn = false;
    protected Energy playerEnergy;
    protected PlayerLightController playerLightController;
    protected float tmpSpeed;

    protected Boss bossObject;
    protected Animator bossAnimator;

    protected virtual void Start()
    {
        curLight = GetComponent<Light2D>();
        SetLightColor();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEnergy = col.gameObject.GetComponent<Energy>();
            playerLightController = col.gameObject.GetComponent<PlayerLightController>();
            originalConsumeSpeed = playerEnergy.GetOriginalConsumeSpeed();
            OnTriggerEnterHelper(playerEnergy, playerLightController);
        } else if (col.gameObject.CompareTag("Boss"))
        {
            bossAnimator = col.gameObject.GetComponent<Animator>();
            bossObject = col.GetComponent<Boss>();
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Energy>().ConsumeSpeed += tmpSpeed * typeOfLight;
            if (tmpSpeed == 0)
            {
                other.gameObject.GetComponent<Energy>().ConsumeSpeed += originalConsumeSpeed;
            }
            other.gameObject.GetComponent<PlayerLightController>().LeaveLight();
            playerIsIn = false;
        }
    }

    protected void SetLightColor()
    {
        if (typeOfLight == HEALING_LIGHT)
        {
            curLight.color = Color.green;
        }
        else if (typeOfLight == NEUTRAL_LIGHT)
        {
            curLight.color = Color.white;
        }
        else
        {
            curLight.color = Color.red;
        }
    }

    protected void OnTriggerEnterHelper(Energy tmpEnergy, PlayerLightController tmpLightController)
    {
        tmpSpeed = Math.Max(Math.Abs(15f * typeOfLight), Math.Abs(originalConsumeSpeed * 2 * typeOfLight));
        if (typeOfLight == HEALING_LIGHT)
        {
            tmpSpeed *= 2;
        }
        
        tmpEnergy.ConsumeSpeed -= tmpSpeed * typeOfLight;
        if (tmpSpeed == 0)
        {
            tmpEnergy.ConsumeSpeed -= originalConsumeSpeed;
        }
        EnterLightHelper(tmpLightController);
        playerIsIn = true;
    }

    protected void EnterLightHelper(PlayerLightController con)
    {
        if (typeOfLight == HEALING_LIGHT)
        {
            con.EnterHealingLight();
        } 
        else if (typeOfLight == NEUTRAL_LIGHT)
        {
            con.EnterNeutralLight();
        }
        else
        {
            con.EnterDamagingLight();
        }
    }
}
