using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDead;
    public event EventHandler OnHealed;
    [Header("Health Settings")]
    [SerializeField] public int MaxHealth = 100;
    [Header("Popup Settings")]
    [SerializeField] private bool popupEnable = true;
    [SerializeField] private Vector3 PopupOffset = new Vector3(0, 2, 0);
    [SerializeField] private Color normalDamageColor = new Color(0.4f, 0.3f, 0.1f, 1f);
    [SerializeField] private Color criticalDamageColor = new Color(0.8f, 0.1f, 0.1f, 1f);
    [SerializeField] private Color healingColor = new Color(0f, 1f, 0f, 1f);
    [SerializeField] private int normalFontSize = 12;
    [SerializeField] private int healingFontSize = 12;
    [SerializeField] private int criticalFontSize = 16;

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

            if (value != curHealth) changeHealth(value);
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

    private Vector3 popupPosition
    {
        get
        {
            return transform.position + PopupOffset;
        }
    }

    private void Awake()
    {
        curHealth = MaxHealth;
    }

    private void changeHealth(int newHealth)
    {
        int healthDelta = newHealth - curHealth;

        // Show Popup
        if (popupEnable) showPopup(healthDelta);

        curHealth = newHealth;

        if (healthDelta < 0)
        {
            if (OnDamaged != null) OnDamaged(this, new IntegerEventArg(-healthDelta));
        }
        else if (newHealth > curHealth)
        {
            if (OnHealed != null) OnHealed(this, new IntegerEventArg(healthDelta));
        }

        if (curHealth == 0)
        {
            // Dead
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }
    }

    private void showPopup(int healthDelta)
    {
        if (healthDelta < 0)
        {
            // Damage
            int damage = -healthDelta;
            bool isCritical = (damage > 10);
            int fontSize = isCritical ? criticalFontSize : normalFontSize;
            Color color = isCritical ? criticalDamageColor : normalDamageColor;

            DamagePopup damagePopup = createPopup(popupPosition);
            damagePopup.Setup(damage, color, fontSize);
        }
        else
        {
            // Heal
            int healing = healthDelta;

            DamagePopup damagePopup = createPopup(popupPosition);
            damagePopup.Setup(healing, healingColor, healingFontSize);
        }
    }

    private static DamagePopup createPopup(Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        return damagePopup;
    }
}
