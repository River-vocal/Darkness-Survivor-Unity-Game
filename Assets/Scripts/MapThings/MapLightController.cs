using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class MapLightController : MonoBehaviour
{
    [Header("Light type Setting")] 
    [SerializeField] private int typeOfLight;
    // 1: healing light, 0: neutral light, -1: damaging Light

    private Light2D curLight;
    private readonly int HEALING_LIGHT = 1;
    private readonly int NEUTRAL_LIGHT = 0;
    private readonly int DAMAGING_LIGHT = -1;
    private float originalConsumeSpeed = 1f;
    
    private void Start()
    {
        curLight = GetComponent<Light2D>();
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            originalConsumeSpeed = col.gameObject.GetComponent<Energy>().ConsumeSpeed;
            float tmpSpeed = Math.Max(Math.Abs(15f * typeOfLight), Math.Abs(originalConsumeSpeed * 2 * typeOfLight));
            col.gameObject.GetComponent<Energy>().ConsumeSpeed = -tmpSpeed * typeOfLight;
            EnterLightHelper(col.gameObject.GetComponent<PlayerLightController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Energy>().ConsumeSpeed = originalConsumeSpeed;
            other.gameObject.GetComponent<PlayerLightController>().LeaveLight();
        }
    }

    private void EnterLightHelper(PlayerLightController con)
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
