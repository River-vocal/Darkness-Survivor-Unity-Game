using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Key script
/// Author: Indie Marc (Marc-Antoine Desbiens)
/// </summary>

namespace IndieMarc.Platformer
{

    public class Key : MonoBehaviour
    {
        public int key_index = 0; //Which door it opens
        public int key_value = 1; //How much of the door it opens

        public AudioClip take_audio;

        private string unique_id;
        private CarryItem carry_item;

        void Start()
        {
            carry_item = GetComponent<CarryItem>();
            carry_item.OnTake += OnTake;
            carry_item.OnDrop += OnDrop;
            

        }

        private void Update()
        {
            
        }

        private void OnTake(GameObject triggerer)
        {
            TheAudio.Instance.PlaySound("key", take_audio);
        }

        private void OnDrop(GameObject triggerer)
        {

        }
        
        public bool TryOpenDoor(Door door)
        {
            if (door && door.CanKeyUnlock(this) && !door.IsOpened())
            {
                door.Open();
                Destroy(gameObject);
                return true;
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Door>())
            {
                TryOpenDoor(collision.gameObject.GetComponent<Door>());
            }
        }
    }

}