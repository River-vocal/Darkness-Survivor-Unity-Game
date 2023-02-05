using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TornadoBanditsStudio
{
    [RequireComponent (typeof (NavMeshAgent))]
    public class TBS_WispBehaviour : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent; //wisp nav mesh agent
        [SerializeField] private float minDistanceToTarget; //min distance to target
        private Transform lastPortalVisited; //last portal visited

        void Awake ()
        {
            //Init nav mesh agent
            if (navMeshAgent != null)
                navMeshAgent = this.GetComponent<NavMeshAgent> ();
        }

        void Start ()
        {
            if (TBS_WispsManager.singleton != null && TBS_WispsManager.singleton.portals.Count >= 2)
                SetNewTarget ();
            else
                Debug.LogError ("You can't play with a wisp if there is not a wisp manager and it has at least 2 portals.");
        }

        void Update ()
        {
            //Check if the wisp is too close to its destination
            if (navMeshAgent.remainingDistance <= minDistanceToTarget)
                SetNewTarget (true);
        }

        /// <summary>
        /// Set a new target
        /// </summary>
        void SetNewTarget (bool teleport = false)
        {
            //Randomize wisp speed
            RandomizeWispAgent ();

            //If is time to teleport then teleport the wisp to a random target.
            if (teleport)
            {
                Transform randomPortal = TBS_WispsManager.singleton.GetRandomPortalTransform ();
                while (randomPortal == lastPortalVisited)
                {
                    randomPortal = TBS_WispsManager.singleton.GetRandomPortalTransform ();
                }
                this.transform.position = randomPortal.position;
            }

            //First get a random target.
            Transform randomTarget = TBS_WispsManager.singleton.GetRandomPortalTransform ();
            while (randomTarget == lastPortalVisited)
            {
                randomTarget = TBS_WispsManager.singleton.GetRandomPortalTransform ();   
            }
            lastPortalVisited = randomTarget;
            navMeshAgent.SetDestination (randomTarget.position);
        }

        /// <summary>
        /// Randomize wisp speed.
        /// </summary>
        public void RandomizeWispAgent ()
        {
            if (TBS_WispsManager.singleton.randomizeWispSpeed)
            {
                navMeshAgent.speed = Random.Range (TBS_WispsManager.singleton.minWispSpeed, TBS_WispsManager.singleton.maxWispSpeed);
            }
        }
    }
}
