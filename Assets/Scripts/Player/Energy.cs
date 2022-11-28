using MyEventArgs;
using System;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public const float InitialMaxEnergy = 100f;
    public VoidEventChannel playerDieEventChannel;

    [Header("Energy Settings")] [SerializeField]
    public float MaxEnergy = 100f;

    private EffectManager healing;
    [SerializeField] private float consumeSpeed = 5f;

    public float ConsumeSpeed
    {
        set
        {
            if (value < 0) healing.StartHealing();
            else healing.StopHealing();
            consumeSpeed = value;
        }
        get => consumeSpeed;
    }

    [SerializeField] public float SlowDownFactor = 0.5f;
    [SerializeField] public float SlowDownThreshold = 0.2f;
    [SerializeField] private float originalConsumeSpeed;

    [Header("Popup Settings")] [SerializeField]
    public float popupThreshold = 10f;

    [SerializeField] public Color damagePopupColor = new Color(0.2f, 0.3f, 0.3f);
    [SerializeField] public Color buffPopupColor = new Color(0, 0.4f, 0.2f);
    [SerializeField] public int energyPopupSize = 10;
    [SerializeField] private Vector3 PopupOffset = new Vector3(0, 2, 0);

    private bool damageable = true;

    public bool Damageable
    {
        set
        {
            damageable = value;
            if (!damageable)
            {
                curEnergy = MaxEnergy;
                healing.StartShield();
            }
            else
            {
                healing.StopShield();
            }

            OnDamageableChanged?.Invoke(this, new BooleanEventArg(value));
        }
        get => damageable;
    }

    public event EventHandler OnDamageableChanged;

    private float curEnergy;

    private void Awake()
    {
        healing = GetComponent<EffectManager>();
        originalConsumeSpeed = ConsumeSpeed;
    }


    private void Start()
    {
        MaxEnergy = LevelLoader.current.EnergyData.MaxEnergy;
        curEnergy = MaxEnergy;
    }

    private void OnDisable()
    {
        LevelLoader.current.EnergyData.MaxEnergy = MaxEnergy;
    }

    public void Kill()
    {
        curEnergy = 0;
        playerDieEventChannel.Broadcast();
    }

    public float CurEnergy
    {
        get => curEnergy;
        set
        {
            if (!damageable && value < curEnergy)
            {
                DamagePopup damagePopup = global::DamagePopup.CreatePopup(PopupPosition);
                damagePopup.Setup("Miss", buffPopupColor, energyPopupSize);
                return;
            }

            value = Math.Min(value, MaxEnergy);
            value = Math.Max(value, 0f);

            if (value > curEnergy)
            {
                healing.HealOnce();
            }
            else if(value < curEnergy)
            {
                DamagePopup(curEnergy - value);
            }

            curEnergy = value;
            if (curEnergy == 0) playerDieEventChannel.Broadcast();
        }
    }


    public float CurEnergyNormalized
    {
        get { return curEnergy / MaxEnergy; }
    }

    private void Update()
    {
        if (!damageable) return;
        if (curEnergy <= 0) return;

        float energyDiff = (CurEnergyNormalized > SlowDownThreshold)
            ? ConsumeSpeed * Time.deltaTime
            : ConsumeSpeed * SlowDownFactor * Time.deltaTime;

        // if (energyDiff < 0)
        // {
        //     GlobalAnalysis.healing_energy -= energyDiff;
        // }
        // else if (speedDiff > 0)
        // {
        //     GlobalAnalysis.light_damage += energyDiff;
        //     //predict the dead cause before CurEnergy changed
        //     if (CurEnergy - energyDiff <= 0.0001f)
        //     {
        //         GlobalAnalysis.player_status = "red_light_dead";
        //         Debug.Log("lose by: red light");
        //     }
        // }

        curEnergy -= energyDiff;
        curEnergy = Math.Min(curEnergy, MaxEnergy);
        curEnergy = Math.Max(curEnergy, 0f);
        if (curEnergy <= 0) playerDieEventChannel.Broadcast();
    }

    public float OriginalConsumeSpeed => originalConsumeSpeed;

    private Vector3 PopupPosition => transform.position + PopupOffset;

    private void DamagePopup(float energyDamage)
    {
        int percentage = (int)(energyDamage / MaxEnergy * 100);

        DamagePopup damagePopup = global::DamagePopup.CreatePopup(PopupPosition);
        damagePopup.Setup($"{percentage}%", damagePopupColor, energyPopupSize);
    }
}