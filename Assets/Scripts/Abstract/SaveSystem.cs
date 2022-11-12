using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveByJson(string saveFileName, object data)
    {
        try
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            Debug.Log($"Save to {path}");
#endif
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError(e);
#endif
        }
    }

    public static T LoadFromJson<T>(string saveFileName)
    {
        try
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
#if UNITY_EDITOR
            Debug.Log($"Load from {path}");
#endif
            return data;
        }
        catch (FileNotFoundException)
        {
            return default;
        }
    }

    public static void DeleteSaveFile(string saveFileName)
    {
        try
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);
            File.Delete(path);
#if UNITY_EDITOR
            Debug.Log($"Delete {path}");
#endif
        }
        catch (FileNotFoundException)
        {
#if UNITY_EDITOR
            Debug.Log($"Attempt to delete not exist file{saveFileName}");
#endif
        }
    }
}