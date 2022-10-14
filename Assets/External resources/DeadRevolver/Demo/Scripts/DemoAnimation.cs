using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadRevolver.PixelPrototypePlayer {
    [System.Serializable]
    public class DemoAnimation : MonoBehaviour {
        public new string name = "";

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}