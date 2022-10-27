using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] public float MaxEnergy = 100f;
    [SerializeField] public float ConsumeSpeed = 1f;
    [SerializeField] public float SlowDownFactor = 0.5f;
    [SerializeField] public float SlowDownThreshold = 0.2f;
    [SerializeField] private float originalConsumeSpeed;

    public event EventHandler OnEmpty;

    private float curEnergy;

    private void Start()
    {
        originalConsumeSpeed = ConsumeSpeed;
    }

    public float CurEnergy
    {
        get
        {
            return curEnergy;
        }
        set
        {
            value = Math.Min(value, MaxEnergy);
            value = Math.Max(value, 0f);
            setEnergy(value);
        }
    }

    private void setEnergy(float value)
    {
        curEnergy = value;
        if (curEnergy <= 0.0001f)
        {
            if (OnEmpty != null) OnEmpty(this, EventArgs.Empty);
        }
    }


    public void TakeDamage(float damage)
    {
        CurEnergy -= damage;
    }


    public float CurEnergyNormalized
    {
        get
        {
            return curEnergy / MaxEnergy;
        }
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
        }else{
            energyDiff = ConsumeSpeed * SlowDownFactor * Time.deltaTime;
        }

        if (energyDiff < 0) 
        {
            GlobalAnalysis.healing_energy -= energyDiff;
        } else if (speedDiff > 0) {
            GlobalAnalysis.light_damage += energyDiff;
            //predict the dead cause before CurEnergy changed
            if (CurEnergy - energyDiff <= 0.0001f) {
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
}
