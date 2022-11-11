using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyPatrol : MonoBehaviour
    {
        public float speed_mult = 1f;
        public float wait_time = 1f;

        [Header("Patrol")]
        public GameObject[] patrol_targets;

        private Enemy enemy;
        private Vector3 start_pos;
        private bool waiting = false;
        private float wait_timer = 0f;

        private int current_path = 0;
        private bool path_rewind = false;
        private Vector3 move_dir;

        private List<Vector3> path_list = new List<Vector3>();

        void Start()
        {
            enemy = GetComponent<Enemy>();
            start_pos = transform.position;
            path_list.Add(transform.position);
            move_dir = Vector3.right * Mathf.Sign(transform.localScale.x);

            foreach (GameObject patrol in patrol_targets)
            {
                if (patrol)
                    path_list.Add(patrol.transform.position);
            }
            
            current_path = 0;
            if (path_list.Count >= 2)
                current_path = 1; //Dont start at start pos
        }

        void Update()
        {
            if (TheGame.IsGamePaused())
                return;

            wait_timer += Time.deltaTime;

            move_dir = Vector3.right * Mathf.Sign(transform.localScale.x);

            //If still in starting path
            if (!waiting && !HasFallen() && path_list.Count > 0)
            {
                //Move
                Vector3 targ = path_list[current_path];
                enemy.MoveTo(targ, speed_mult);
                enemy.FaceToward(targ);
                move_dir = Vector3.right * Mathf.Sign((targ - transform.position).x);

                //Check if reached target
                Vector3 dist_vect = (targ - transform.position);
                dist_vect.z = 0f;
                if (dist_vect.magnitude < 0.1f)
                {
                    waiting = true;
                    wait_timer = 0f;
                }

                //Check if obstacle ahead
                bool fronted = enemy.CheckFronted(dist_vect.normalized);
                if (fronted && wait_timer > 2f)
                {
                    RewindPath();
                    wait_timer = 0f;
                }
            }

            //If can't reach starting path anymore
            if (!waiting && HasFallen())
            {
                //Move
                Vector3 mdir = Vector3.right * (path_rewind ? -2f : 2f);
                Vector3 targ = transform.position + mdir;
                enemy.MoveTo(targ, speed_mult);
                enemy.FaceToward(targ);
                move_dir = Vector3.right * Mathf.Sign((targ - transform.position).x);

                //Check if obstacle ahead
                Vector3 dist_vect = (targ - transform.position);
                bool fronted = enemy.CheckFronted(dist_vect.normalized);
                if (fronted && wait_timer > 2f)
                {
                    path_rewind = !path_rewind;
                    wait_timer = 0f;
                }
            }

            if (waiting)
            {
                //Wait a bit
                if (wait_timer > wait_time)
                {
                    GoToNextPath();
                    waiting = false;
                    wait_timer = 0f;
                }
            }
        }

        private void RewindPath()
        {
            path_rewind = !path_rewind;
            current_path += path_rewind ? -1 : 1;
            current_path = Mathf.Clamp(current_path, 0, path_list.Count - 1);
        }

        private void GoToNextPath()
        {
            if (current_path <= 0 || current_path >= path_list.Count - 1)
                path_rewind = !path_rewind;
            current_path += path_rewind ? -1 : 1;
            current_path = Mathf.Clamp(current_path, 0, path_list.Count - 1);
        }
        
        public bool HasFallen()
        {
            float distY = Mathf.Abs(transform.position.y - start_pos.y);
            return !enemy.flying && distY > 0.5f;
        }

        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 prev_pos = transform.position;

            foreach (GameObject patrol in patrol_targets)
            {
                if (patrol)
                {
                    Gizmos.DrawLine(prev_pos, patrol.transform.position);
                    prev_pos = patrol.transform.position;
                }
            }
        }

        public Enemy GetEnemy()
        {
            return enemy;
        }

        public Vector3 GetMove()
        {
            return move_dir;
        }
    }
}