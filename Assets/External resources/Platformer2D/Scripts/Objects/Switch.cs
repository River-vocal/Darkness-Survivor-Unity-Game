using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndieMarc.Platformer
{

    public class Switch : MonoBehaviour
    {

        public int door_value;
        public string tag_required;
        public AudioClip activate_sound;

        private bool activated = false;
        private Animator animator;
        private HashSet<Collider2D> collisions = new HashSet<Collider2D>();
        private float init_timer = 0f;

        void Start()
        {
            animator = GetComponent<Animator>();
            init_timer = -1f;
        }

        void Update()
        {
            activated = (collisions.Count > 0);
            animator.SetBool("Active", activated);

            if (init_timer < 0f)
                init_timer += Time.deltaTime;

            //Remove destroyed colliders
            foreach (Collider2D collision in collisions)
            {
                float dist = collision ? (collision.transform.position - transform.position).magnitude : 0f;
                if (collision == null || !collision.enabled || dist > 2f)
                {
                    collisions.Remove(collision);
                    break; //Remove others in next frame to avoid breaking the loop
                }
            }
        }

        public void Destroy()
        {
            activated = false;
            gameObject.SetActive(false);
        }

        public void Revive()
        {
            gameObject.SetActive(true);
        }

        public bool IsActive()
        {
            return activated;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (tag_required == "" || tag_required == collision.gameObject.tag)
            {
                collisions.Add(collision);
                if (init_timer >= 0f)
                    TheAudio.Play("switch", activate_sound, 0.5f);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            collisions.Remove(collision);
        }

    }

}