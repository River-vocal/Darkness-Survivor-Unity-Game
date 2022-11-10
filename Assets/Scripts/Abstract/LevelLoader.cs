using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public StringEventChannel extraEnergyLoadedEventChannel;
    [SerializeField] public StringEventChannel onEnergyPickupCollectedEventChannel;
    public static string CurrentLevelName => SceneManager.GetActiveScene().name;
    public static LevelLoader current;

    private string LevelDataFilename => $"{CurrentLevelName}.sav";
    private const string EnergyDataFilename = "Energy.sav";

    public LevelData LevelData;
    public EnergyData EnergyData;

    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        LevelData = SaveSystem.LoadFromJson<LevelData>(LevelDataFilename) ?? new LevelData();
        EnergyData = SaveSystem.LoadFromJson<EnergyData>(EnergyDataFilename) ?? new EnergyData();
        onEnergyPickupCollectedEventChannel.AddListener(AddToData);
    }

    private void Start()
    {
        foreach (var pickupId in LevelData.ExtraEnergyPickupsCollected)
        {
            extraEnergyLoadedEventChannel.Broadcast(pickupId);
        }
    }

    private void OnDisable()
    {
        onEnergyPickupCollectedEventChannel.RemoveListener(AddToData);
        SaveSystem.SaveByJson(LevelDataFilename, LevelData);
        SaveSystem.SaveByJson(EnergyDataFilename, EnergyData);
    }

    private void AddToData(String id)
    {
        Debug.Log($"Add to data{id}");
        LevelData.ExtraEnergyPickupsCollected.Add(id);
    }
}