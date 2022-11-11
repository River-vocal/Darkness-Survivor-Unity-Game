using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class HazardDamage : MonoBehaviour
    {
        public float damage = 5f;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerCharacter character = collision.GetComponent<PlayerCharacter>();
            if (character)
                character.TakeDamage(damage);
        }
    }

}
