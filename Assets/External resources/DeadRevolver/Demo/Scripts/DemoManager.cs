using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DeadRevolver.PixelPrototypePlayer {
    public class DemoManager : MonoBehaviour {
        public List<DemoAnimation> animations = new List<DemoAnimation>();
        public UnityEvent<DemoAnimation> onAnimationChanged;
        public UnityEvent onAnimationPrev;
        public UnityEvent onAnimationNext;

        private int _currentAnimation = 0;

        void Start() {
            UpdateAnimation();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                PreviousAnimation();
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                NextAnimation();
            }
        }

        private void PreviousAnimation() {
            _currentAnimation--;

            if (_currentAnimation < 0) {
                _currentAnimation = animations.Count - 1;
            }

            onAnimationPrev.Invoke();
            UpdateAnimation();
        }

        private void NextAnimation() {
            _currentAnimation++;

            if (_currentAnimation > animations.Count - 1) {
                _currentAnimation = 0;
            }

            onAnimationNext.Invoke();
            UpdateAnimation();
        }

        private void UpdateAnimation() {
            DemoAnimation currentAnimation = animations[_currentAnimation];
            animations.FindAll((animation) => { return animation != currentAnimation; }).ForEach((animation) => { animation.Hide(); });
            currentAnimation.Show();
            onAnimationChanged.Invoke(currentAnimation);
        }
    }

}