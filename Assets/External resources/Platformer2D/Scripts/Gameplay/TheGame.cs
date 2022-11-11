using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndieMarc.Platformer
{
    public class TheGame : MonoBehaviour
    {
        public AudioClip start_sfx;
        public AudioClip music;

        private bool paused = false;
        private bool ended = false;

        public UnityAction<bool> onPause;

        private static TheGame _instance;
        
        void Awake()
        {
            _instance = this;
            PlayerData.AutoNew();
        }

        private void Start()
        {
            //Load game data
            PlayerData pdata = PlayerData.Get();
            if (!string.IsNullOrEmpty(pdata.current_scene))
            {
                if (!string.IsNullOrEmpty(pdata.current_checkpoint))
                {
                    //Go to checkpoint
                    Checkpoint checkpoint = Checkpoint.Get(pdata.current_checkpoint);
                    if (checkpoint)
                        MoveCharacterTo(checkpoint.transform.position);
                }
                else
                {
                    //Go to entry
                    LevelExit exit = LevelExit.Get(pdata.current_entry_index);
                    if (exit != null)
                        MoveCharacterTo(exit.transform.position + new Vector3(exit.entrance_offset.x, exit.entrance_offset.y));
                }
            }

            //Pause game
            if (PauseMenu.Get())
            {
                PauseMenu.Get().onShow += () => { Pause(); };
                PauseMenu.Get().onHide += () => { Unpause(); };
            }

            //Camera init
            FollowCamera cam = FollowCamera.Get();
            if (cam != null && cam.target != null)
                cam.MoveTo(cam.target.transform.position);

            //Audio
            TheAudio.Instance.PlaySound("level", start_sfx);
            TheAudio.Instance.PlaySoundLoop("music", music);

            //Black Panel
            BlackPanel.Get().Show(true);
            BlackPanel.Get().Hide();
        }

        void MoveCharacterTo(Vector3 pos)
        {
            foreach (PlayerCharacter character in PlayerCharacter.GetAll())
                character.Teleport(pos);
        }

        void Update()
        {
            //Check for death
            List<PlayerCharacter> characters = PlayerCharacter.GetAll();
            if (!ended && characters.Count > 0)
            {
                int nb_alive = characters.Count;
                foreach (PlayerCharacter character in characters)
                {
                    if (character.IsDead())
                        nb_alive--;
                }
                if (nb_alive == 0)
                {
                    ended = true;
                    StartCoroutine(EndGameRoutine());
                }
            }

            //Open menu
            PauseMenu menu = PauseMenu.Get();
            foreach (PlayerControls controls in PlayerControls.GetAll())
            {
                if (menu && controls.GetMenuDown())
                {
                    menu.Toggle();
                }
            }
        }

        public void Pause()
        {
            paused = true;
            if (onPause != null)
                onPause.Invoke(paused);
        }

        public void Unpause()
        {
            paused = false;
            if (onPause != null)
                onPause.Invoke(paused);
        }

        public bool IsPaused()
        {
            return paused;
        }
        
        public void Save()
        {
            PlayerData.Get().current_scene = SceneNav.GetCurrentScene();
            PlayerData.Save();
        }

        public IEnumerator EndGameRoutine()
        {
            yield return new WaitForSeconds(1f);
            GameOver.Get().Show();
            yield return new WaitForSeconds(3f);
            PlayerData.Unload(); //Reload previous data
            SceneNav.RestartLevel();
        }

        public static bool IsGamePaused()
        {
            if (_instance)
                return _instance.IsPaused();
            return false;
        }

        public static TheGame Get()
        {
            return _instance;
        }
    }
}
