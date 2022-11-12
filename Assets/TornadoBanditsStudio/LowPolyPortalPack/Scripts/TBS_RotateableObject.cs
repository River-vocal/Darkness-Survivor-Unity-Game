using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TornadoBanditsStudio
{
    public class TBS_RotateableObject : MonoBehaviour
    {
        //Rotation axis
        private enum ROTATION_AXIS
        {
            X,
            Y,
            Z
        }

        [SerializeField] private ROTATION_AXIS rotationAxis; //chosen rotation axe
        [SerializeField] private float rotationSpeed = 60f; //rotation speed
        [SerializeField] private bool randomSpeed = false; //randomize speed
        [SerializeField] private bool randomDirection = true; //randomzie direction

        void Start ()
        {
            //Randomize speed and direction
            if (randomSpeed)
                rotationSpeed = Random.Range (rotationSpeed - rotationSpeed / 3, rotationSpeed + rotationSpeed / 3);

            if (randomDirection)
            {
                int randomDir = Random.Range (0, 2);
                if (randomDir == 1)
                    rotationSpeed *= -1;
            }
        }

        void Update ()
        {
            //Rotate based on chosen rotation axis.
            switch (rotationAxis)
            {
                case ROTATION_AXIS.X:
                    this.transform.Rotate (rotationSpeed * Time.deltaTime, 0f, 0f);
                    break;

                case ROTATION_AXIS.Y:
                    this.transform.Rotate (0f, rotationSpeed * Time.deltaTime, 0f);
                    break;

                case ROTATION_AXIS.Z:
                    this.transform.Rotate (0f, 0f, rotationSpeed * Time.deltaTime);
                    break;

                default:
                    break;
            }
        }
    }
}
