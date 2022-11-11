using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace IndieMarc.Platformer
{
    [System.Serializable]
    public class SaveData
    {
        public Dictionary<string, PlayerData> saved_games = new Dictionary<string, PlayerData>();
        public string last_save = "";
    }

    [System.Serializable]
    public class SaveSystem
    {
        private static bool loaded = false;
        private static bool saving = false;

        public static SaveData saved_data = new SaveData();

        private static void LoadData()
        {
            if (!loaded && File.Exists(Application.persistentDataPath + "/player_save.data"))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + "/player_save.data", FileMode.Open);
                    saved_data = (SaveData)bf.Deserialize(file);
                    file.Close();
                }
                catch (System.Exception e) { Debug.Log("Error Loading Data " + e); }
            }
            loaded = true;
        }

        private static void SaveData()
        {

            if (loaded)
            {
                saving = true;
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Create(Application.persistentDataPath + "/player_save.data");
                    bf.Serialize(file, saved_data);
                    file.Close();
                }
                catch (System.Exception e) { Debug.Log("Error Saving Data " + e); }
                saving = false;
            }
        }

        public static string GetLastSave()
        {
            LoadData();
            return saved_data.last_save;
        }

        public static bool HasLoad(string save_name)
        {
            LoadData();
            return saved_data.saved_games.ContainsKey(save_name);
        }

        public static PlayerData Load(string save_name)
        {
            LoadData();

            if (saved_data.saved_games.ContainsKey(save_name))
            {
                return saved_data.saved_games[save_name];
            }
            return null;
        }

        public static void Unload()
        {
            loaded = false;
        }

        public static void Save(string save_name, PlayerData player)
        {
            LoadData();

            if (save_name != "")
            {
                saved_data.saved_games[save_name] = player;
                saved_data.last_save = save_name;
                SaveData();
            }
        }

        public static void Delete(string save_name)
        {
            LoadData();

            if (save_name != "")
            {
                saved_data.saved_games.Remove(save_name);
                saved_data.last_save = "";
                SaveData();
            }
        }

        public static bool IsSaving()
        {
            return saving;
        }
    }

}