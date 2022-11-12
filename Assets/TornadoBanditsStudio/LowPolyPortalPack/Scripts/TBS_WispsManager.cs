using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TornadoBanditsStudio
{
    public class TBS_WispsManager : MonoBehaviour
    {
        public static TBS_WispsManager singleton { get; private set; }
        
        [Header ("Portals transform reference")]
        public List<Transform> portals = new List<Transform>(); //List of portals available in this scene

        [Space (15)]
        [Header ("Wisp settings")]
        [SerializeField] private int numberOfWispsToBeSpawned; //Number of wisps to be spawned
        [SerializeField] private TBS_WispBehaviour wispPrefab; //Wisp prefab
        public bool randomizeWispSpeed = true; //Randomize wisps speed
        public float minWispSpeed = 5f; //Min wisp speed
        public float maxWispSpeed = 15f; //Max wisp speed

        void Awake ()
        {
            //Singleton
            if (singleton == null)
                singleton = this;
            else
                DestroyImmediate (this.gameObject);
        }

        void Start ()
        {
            //Spawn wips based on the number of wisps set for this scene. but only if there is more than one portal so wips won't get stuck
            if (portals.Count >= 2)
                SpawnWisps ();
        }

        /// <summary>
        /// Spawn wisps.
        /// </summary>
        void SpawnWisps ()
        {
            //If we have a prefab then spawn them
            if (wispPrefab != null)
            {
                //Spawn the number of wisps set in inspector
                for (int i=0; i<numberOfWispsToBeSpawned; i++)
                {
                    TBS_WispBehaviour newWisp = (TBS_WispBehaviour)Instantiate (wispPrefab, GetRandomPortalTransform ().position, Quaternion.identity);
                }
            } else
            {
                Debug.LogError ("Please set the wisp prefab.");
            }
        }

        /// <summary>
        /// Returns a random portal position
        /// </summary>
        /// <returns></returns>
        public Transform GetRandomPortalTransform ()
        {
            return (portals[(int)Random.Range (0, portals.Count)]);
        }
    }
}
