using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public enum PlatformMovingType
    {
        Static = 0,
        Move = 5,
        Fall = 10,
    }

    public class PlatformMoving : MonoBehaviour
    {
        public PlatformMovingType type;
        public Vector2 move_vect;
        public float move_speed = 2f;
        public float fall_delay = 1f;
        public float respawn_delay = 10f;

        private Animator animator;
        private Collider2D collide;
        private Vector3 start_pos;
        private Vector3 move = Vector3.zero;
        private float timer = 0f;
        private bool touched = false;
        private bool move_side = false;

        void Awake()
        {
            animator = GetComponent<Animator>();
            collide = GetComponent<Collider2D>();
            start_pos = transform.position;
        }

        void Update()
        {
            if (type == PlatformMovingType.Move)
            {
                Vector3 target = move_side ? start_pos : start_pos + new Vector3(move_vect.x, move_vect.y, 0f);
                Vector3 dir = target - transform.position;
                float dir_move = Mathf.Min(dir.magnitude, move_speed * Time.deltaTime);
                move = dir.normalized * move_speed;
                transform.position += dir.normalized * dir_move;

                if (dir.magnitude < 0.1f)
                    move_side = !move_side;
            }

            if (type == PlatformMovingType.Fall)
            {
                if (touched)
                {
                    timer += Time.deltaTime;
                    if (timer > fall_delay)
                    {
                        move = Vector3.down * move_speed;
                        transform.position += Vector3.down * move_speed * Time.deltaTime;
                    }
                    else
                    {
                        transform.position = start_pos + new Vector3(Mathf.Cos(timer * 8f * Mathf.PI) * 0.05f, 0f, 0f);
                    }

                    if (timer > respawn_delay)
                    {
                        ResetPlatform();
                    }
                }
            }
        }

        public void ResetPlatform()
        {
            timer = 0f;
            transform.position = start_pos;
            collide.enabled = true;
            touched = false;
            move = Vector3.zero;

            if (animator)
                animator.Rebind();
        }

        public void OnCharacterStep()
        {
            if (!touched)
            {
                touched = true;
                timer = 0f;
            }
        }

        public Vector3 GetMove()
        {
            return move;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                if (dir.y > 0f)
                {
                    OnCharacterStep();
                }
            }
        }
    }

}