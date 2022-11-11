using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{

    public enum PowerupType
    {
        None=0,
        Life=5,
        Coin=10,
    }

    public class Powerup : MonoBehaviour
    {
        public PowerupType type;
        public float value;

        public AudioClip take_audio;

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void Take(PlayerCharacter character)
        {
            PlayerData pdata = PlayerData.Get();
            if (type == PowerupType.Life)
            {
                character.HealDamage(value);
            }
            if (type == PowerupType.Coin)
            {
                pdata.coins += Mathf.RoundToInt(value);
            }

            if (TheAudio.Instance)
                TheAudio.Instance.PlaySound("powerup", take_audio, 0.5f);

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerCharacter>())
                Take(collision.GetComponent<PlayerCharacter>());
        }
    }

}