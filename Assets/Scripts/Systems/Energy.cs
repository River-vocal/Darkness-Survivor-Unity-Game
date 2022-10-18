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
        float prevEnergy = CurEnergy;
        if (CurEnergyNormalized > SlowDownThreshold)
        {
            CurEnergy -= ConsumeSpeed * Time.deltaTime;
        }else{
            CurEnergy -= ConsumeSpeed * SlowDownFactor * Time.deltaTime;
        }

        if (ConsumeSpeed < 0) 
        {
            GlobalAnalysis.healing_energy += CurEnergy - prevEnergy;
        } else if (speedDiff > 0) 
        {
            GlobalAnalysis.light_damage += prevEnergy - CurEnergy;
            if (curEnergy <= 0.0001f) {
                GlobalAnalysis.player_status = "red_light_dead";
                Debug.Log("lose by: red light");
            }
        } 


        
    }

    public float GetOriginalConsumeSpeed()
    {
        return originalConsumeSpeed;
    }
}
