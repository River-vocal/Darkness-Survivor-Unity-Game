using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class StartScene : MonoBehaviour
    {
        public string first_level;

        void Awake()
        {
            PlayerData.Load();

        }

        void Update()
        {

        }

        public void OnClickContinue()
        {
            PlayerData pdata = PlayerData.Get();
            if (!string.IsNullOrEmpty(pdata.current_scene))
            {
                if (!string.IsNullOrEmpty(pdata.current_checkpoint))
                    SceneNav.GoToCheckpoint(pdata.current_scene, pdata.current_checkpoint);
                else
                    SceneNav.GoToLevel(pdata.current_scene, pdata.current_entry_index);
            }
        }

        public void OnClickNew()
        {
            PlayerData.NewGame();
            PlayerData.Save(); //Overwrite existing game
            SceneNav.GoToLevel(first_level);
        }
    }

}
