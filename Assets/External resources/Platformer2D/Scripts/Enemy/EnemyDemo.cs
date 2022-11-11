using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyDemo : MonoBehaviour
    {
        public GameObject death_fx_prefab;
        
        private Animator animator;
        private Enemy enemy;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemy = GetComponent<Enemy>();

            enemy.onDeath += OnDeath;
        }

        void Update()
        {

        }

        private void OnDeath()
        {
            if(death_fx_prefab)
                Instantiate(death_fx_prefab, transform.position + Vector3.up * 0.5f, death_fx_prefab.transform.rotation);
            animator.SetTrigger("Death");
        }
    }
}
