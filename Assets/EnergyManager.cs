using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    public static float MaxEnergySave = 50;
    public static float HealthBarWidthSave = 300;

    [SerializeField] private RectTransform energyBar;
    [SerializeField] private Energy energy;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(energy.MaxEnergy < MaxEnergySave){
            energy.MaxEnergy = MaxEnergySave;
            energy.CurEnergy = MaxEnergySave;
        }
        if(energyBar.rect.width < HealthBarWidthSave){
            energyBar.GetComponent<EnergyBarResizer>().Resize(HealthBarWidthSave);
        }
    }

    private void OnDisable() {
        MaxEnergySave = Mathf.Max(MaxEnergySave, energy.MaxEnergy);
        HealthBarWidthSave = Mathf.Max(energyBar.rect.width, HealthBarWidthSave);
    }
}
