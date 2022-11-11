using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyFollow : MonoBehaviour
    {
        public float speed_mult = 1f;
        public float follow_range = 5f;
        public GameObject target;
        
        private Enemy enemy;

        void Start()
        {
            enemy = GetComponent<Enemy>();
            
        }

        void Update()
        {
            if (TheGame.IsGamePaused())
                return;

            if (target == null)
                return;

            Vector3 targ = target.transform.position;
            Vector3 dir_vect = targ - transform.position;
            if (dir_vect.magnitude < follow_range)
            {
                enemy.MoveTo(targ, speed_mult);
                enemy.FaceToward(enemy.GetMoveTarget(), 2f);
            }
        }

        public Enemy GetEnemy()
        {
            return enemy;
        }
    }

}