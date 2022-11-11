using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class BlackPanel : UIPanel
    {
        private static BlackPanel instance;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
        }

        public static BlackPanel Get()
        {
            return instance;
        }
    }
}
