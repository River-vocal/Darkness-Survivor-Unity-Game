using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static string CurrentLevelName => SceneManager.GetActiveScene().name;
    public static LevelLoader current;

    private string LevelDataFilename => $"{CurrentLevelName}.sav";
    private const string EnergyDataFilename = "Energy.sav";

    public LevelData LevelData;
    public EnergyData EnergyData;

    private void Awake()
    {
        current = this;
        LevelData = SaveSystem.LoadFromJson<LevelData>(LevelDataFilename) ?? new LevelData();
        EnergyData = SaveSystem.LoadFromJson<EnergyData>(EnergyDataFilename) ?? new EnergyData();
    }

    private void OnDestroy()
    {
        SaveSystem.SaveByJson(LevelDataFilename, LevelData);
        SaveSystem.SaveByJson(EnergyDataFilename, EnergyData);
    }
}