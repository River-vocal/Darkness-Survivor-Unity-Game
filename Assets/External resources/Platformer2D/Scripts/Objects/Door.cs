using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Door script
/// Author: Indie Marc (Marc-Antoine Desbiens)
/// </summary>
/// 

namespace IndieMarc.Platformer
{

    public class Door : MonoBehaviour
    {
        public bool opened_at_start = false;
        public float speed = 4f;
        public float max_move = 2f;

        [Header("Triggers")]
        public int nb_triggers = 1;
        public GameObject[] triggers;

        [Header("Key")]
        public bool key_can_open = false;
        public int key_index = 0;

        [Header("Audio")]
        public AudioClip audio_door_open;
        public AudioClip audio_door_close;

        //private Animator animator;
        //private Collider2D collide;
        private UniqueID unique_id;

        private bool is_opened = false;
        private Vector3 start_pos;
        private Vector3 target_pos;

        private List<Lever> lever_list = new List<Lever>();
        private List<Switch> switch_list = new List<Switch>();

        void Awake()
        {
            //animator = GetComponent<Animator>();
            //collide = GetComponent<Collider2D>();
            unique_id = GetComponent<UniqueID>();
            start_pos = transform.position;
            target_pos = transform.position;

            foreach (GameObject trigger in triggers)
            {
                if (trigger)
                {

                    Lever alever = trigger.GetComponent<Lever>();
                    if (alever)
                        lever_list.Add(alever);

                    Switch aswitch = trigger.GetComponent<Switch>();
                    if (aswitch)
                        switch_list.Add(aswitch);
                }
            }
        }

        private void Start()
        {
            if (opened_at_start)
                Open();

            if (unique_id && unique_id.HasValue())
            {
                if (unique_id.GetValue() > 0)
                    Open();
                else
                    Close();
            }
        }

        void Update()
        {
            if (nb_triggers > 0)
            {
                //Door controlled by triggers
                int nb = 0;

                foreach (Lever aswitch in lever_list)
                {
                    if (aswitch.IsActive())
                        nb++;
                }

                foreach (Switch aswitch in switch_list)
                {
                    if (aswitch.IsActive())
                        nb++;
                }

                bool should_open = opened_at_start ? (nb < nb_triggers) : (nb >= nb_triggers);
                if (should_open && !is_opened)
                    Open();
                if (!should_open && is_opened)
                    Close();
            }
            
            Vector3 move_dir = target_pos - transform.position;
            if (move_dir.magnitude > 0.01f)
            {
                float move_dist = Mathf.Min(speed * Time.deltaTime, move_dir.magnitude);
                transform.position += move_dir.normalized * move_dist;
            }
        }

        public void Open()
        {
            if (!is_opened)
            {
                is_opened = true;
                //collide.enabled = false;
                Vector3 move_dir = transform.up;
                target_pos = start_pos + move_dir.normalized * max_move;
                target_pos.z = 0f;

                //animator.SetBool("Open", true);
                TheAudio.Play("door", audio_door_open, 0.5f);

                if (unique_id)
                    unique_id.SetValue(1);
            }
        }

        public void Close()
        {
            if (is_opened)
            {
                is_opened = false;
                //collide.enabled = true;
                target_pos = start_pos;

                //animator.SetBool("Open", false);
                TheAudio.Play("door", audio_door_close, 0.5f);

                if (unique_id)
                    unique_id.SetValue(0);
            }
        }

        public bool CanKeyUnlock(Key key)
        {
            return (key_can_open && key.key_index == key_index);
        }

        public bool IsOpened()
        {
            return is_opened;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                //TryOpenWithKey();
            }
        }
    }

}