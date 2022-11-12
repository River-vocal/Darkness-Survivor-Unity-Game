using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IndieMarc.Platformer
{
    public class SceneNav
    {
        public static string load_scene;

        public static void GoToLevel(string level_name, int entry_index = 0)
        {
            PlayerData pdata = PlayerData.Get();
            if (pdata != null && level_name != "")
            {
                pdata.current_scene = level_name;
                pdata.current_entry_index = entry_index;
                pdata.current_checkpoint = ""; //Go to checkpoint, not entry

                PlayerData.Save();
                GoToLoading(level_name);
            }
        }

        public static void GoToCheckpoint(string level_name, string checkpoint_uid)
        {
            PlayerData pdata = PlayerData.Get();
            if (pdata != null && level_name != "")
            {
                pdata.current_scene = level_name;
                pdata.current_checkpoint = checkpoint_uid;

                PlayerData.Save();
                GoToLoading(level_name);
            }
        }
        
        public static void RestartLevel()
        {
            GoToLoading(SceneManager.GetActiveScene().name);
        }

        public static void GoToLoading(string load)
        {
            load_scene = load;
            SceneManager.LoadScene("Load");
        }

        public static void ExitToStart()
        {
            SceneManager.LoadScene("Start");
        }

        public static void GoTo(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public static string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }
    }

}