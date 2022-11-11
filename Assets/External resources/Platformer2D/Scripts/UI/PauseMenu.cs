using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class PauseMenu : UIPanel
    {
        private static PauseMenu _instance;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }
        
        public void OnClickResume()
        {
            Hide();
        }

        public void OnClickSave()
        {
            if (TheGame.Get())
                TheGame.Get().Save();
        }

        public void OnClickQuit()
        {
            SceneNav.ExitToStart();
        }

        public static PauseMenu Get()
        {
            return _instance;
        }
    }
}
