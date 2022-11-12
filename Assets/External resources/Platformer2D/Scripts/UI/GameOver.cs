using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{

    public class GameOver : UIPanel {

        private static GameOver _instance;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        public static GameOver Get()
        {
            return _instance;
        }
    }

}