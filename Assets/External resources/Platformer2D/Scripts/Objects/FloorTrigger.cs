using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndieMarc.Platformer
{
    public class FloorTrigger : MonoBehaviour
    {
        public float activate_delay = 1f;

        public UnityAction onStep;
        public UnityAction onStepUpdate;

        private bool activated = false;
        private float activate_timer = 0f;

        void Start()
        {

        }

        void Update()
        {
            activate_timer += Time.deltaTime;
            if (activated && activate_timer > activate_delay)
            {
                activated = false;
            }
        }

        public void Activate()
        {
            if (!activated)
            {
                activated = true;
                if (onStep != null)
                    onStep.Invoke();
            }
            activate_timer = 0f;

            if (onStepUpdate != null)
                onStepUpdate.Invoke();
        }

        public bool IsActivated()
        {
            return activated;
        }
    }

}