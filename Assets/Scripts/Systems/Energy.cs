using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [Header("Energy Settings")]

    [SerializeField] public float MaxEnergy = 100f;
    [SerializeField] public float ConsumeSpeed = 1f;
    [SerializeField] public float SlowDownFactor = 0.5f;
    [SerializeField] public float SlowDownThreshold = 0.2f;
    [SerializeField] private float originalConsumeSpeed;
    
    [Header("Popup Settings")]

    [SerializeField] public float popupThreshold = 10f;
    [SerializeField] public Color energyPopupColor = new Color(0.2f, 0.3f, 0.3f);
    [SerializeField] public int energyPopupSize = 10;
    [SerializeField] private Vector3 PopupOffset = new Vector3(0, 2, 0);


    public event EventHandler OnEmpty;

    private float curEnergy;

    private void Start()
    {
        originalConsumeSpeed = ConsumeSpeed;
    }

    public float CurEnergy
    {
        get { return curEnergy; }
        set
        {
            value = Math.Min(value, MaxEnergy);
            value = Math.Max(value, 0f);
            if (curEnergy - value >= popupThreshold)
            {
                DamagePopup(curEnergy - value);
            }
            curEnergy = value;
            if (curEnergy <= 0.0001f)
            {
                OnEmpty?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        CurEnergy -= damage;
    }


    public float CurEnergyNormalized
    {
        get { return curEnergy / MaxEnergy; }
    }

    private void Awake()
    {
        curEnergy = MaxEnergy;
    }

    private void Update()
    {
        if (CurEnergy == 0) return;
        float speedDiff = ConsumeSpeed - originalConsumeSpeed;
        float energyDiff = 0;
        if (CurEnergyNormalized > SlowDownThreshold)
        {
            energyDiff = ConsumeSpeed * Time.deltaTime;
        }
        else
        {
            energyDiff = ConsumeSpeed * SlowDownFactor * Time.deltaTime;
        }

        if (energyDiff < 0)
        {
            GlobalAnalysis.healing_energy -= energyDiff;
        }
        else if (speedDiff > 0)
        {
            GlobalAnalysis.light_damage += energyDiff;
            //predict the dead cause before CurEnergy changed
            if (CurEnergy - energyDiff <= 0.0001f)
            {
                GlobalAnalysis.player_status = "red_light_dead";
                Debug.Log("lose by: red light");
            }
        }

        CurEnergy -= energyDiff;
    }
    
    public float GetOriginalConsumeSpeed()
    {
        return originalConsumeSpeed;
    }

    private Vector3 PopupPosition => transform.position + PopupOffset;

    private void DamagePopup(float energyDamage)
    {
        int percentage = (int)(energyDamage / MaxEnergy * 100);

        DamagePopup damagePopup = global::DamagePopup.CreatePopup(PopupPosition);
        damagePopup.Setup($"{percentage}%", energyPopupColor, energyPopupSize);
    }
}