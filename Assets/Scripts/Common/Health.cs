using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDead;
    public event EventHandler OnHealed;
    [SerializeField] public int MaxHealth = 100;

    public int CurHealth
    {
        get
        {
            return curHealth;
        }

        set
        {
            value = Math.Min(value, MaxHealth);
            value = Math.Max(0, value);
            
            if (value < curHealth)
            {
                // Damage
                if (OnDamaged != null) OnDamaged(this, new IntegerEventArg(curHealth-value));
            }
            else if (value > curHealth)
            {
                // Healing
                if (OnHealed != null) OnHealed(this, new IntegerEventArg(value-curHealth));
            }

            curHealth = value;

            if (curHealth == 0)
            {
                // Dead
                if (OnDead != null) OnDead(this, EventArgs.Empty);
            }
        }
    }

    private int curHealth;

    public float CurHealthNormalized
    {
        get
        {
            return (float)curHealth / MaxHealth;
        }
    }

    private void Awake() {
        curHealth = MaxHealth;
    }
}
