using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DeadRevolver.PixelPrototypePlayer {
    public class DemoUIManager : MonoBehaviour {
        public TextMeshProUGUI label;

        public void OnAnimationChanged(DemoAnimation animation) {
            label.text = animation.name.ToUpper();
        }
    }
}