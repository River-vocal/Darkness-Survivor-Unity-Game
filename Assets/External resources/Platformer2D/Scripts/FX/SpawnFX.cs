using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{

    public class SpawnFX : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 5f);
        }
    }

}