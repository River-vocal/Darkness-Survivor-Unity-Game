using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    [RequireComponent(typeof(PlayerCharacter))]
    [RequireComponent(typeof(Animator))]
    public class CharacterAnim : MonoBehaviour
    {
        private PlayerCharacter character;
        private CharacterHoldItem character_item;
        private SpriteRenderer render;
        private Animator animator;
        private float flash_fx_timer;

        void Awake()
        {
            character = GetComponent<PlayerCharacter>();
            character_item = GetComponent<CharacterHoldItem>();
            render = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            character.onJump += OnJump;
            character.onCrouch += OnCrouch;
            character.onHit += OnDamage;
            character.onDeath += OnDeath;
            character.onClimb += OnClimb;
        }

        private void Start()
        {
            TheGame.Get().onPause += (bool paused) => { animator.Rebind(); };
        }

        void Update()
        {
            if (TheGame.Get().IsPaused())
                return;

            //Anims
            animator.SetBool("Jumping", character.IsJumping());
            animator.SetBool("InAir", !character.IsGrounded());
            animator.SetBool("Crouching", character.IsCrouching());
            animator.SetFloat("Speed", character.GetMove().magnitude);
            animator.SetBool("Climb", character.IsClimbing());
            if (character_item != null)
                animator.SetBool("Hold", character_item.GetHeldItem() != null);

            //Hit flashing
            render.color = new Color(render.color.r, render.color.g, render.color.b, 1f);
            if (flash_fx_timer > 0f)
            {
                flash_fx_timer -= Time.deltaTime;
                float alpha = Mathf.Abs(Mathf.Sin(flash_fx_timer * Mathf.PI * 4f));
                render.color = new Color(render.color.r, render.color.g, render.color.b, alpha);
            }
        }

        void OnClimb()
        {
            animator.SetTrigger("ChangeState");
            animator.SetBool("Climb", true);
        }

        void OnCrouch()
        {
            animator.SetTrigger("Crouch");
        }

        void OnJump()
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
        }

        void OnDamage()
        {
            if (!character.IsDead())
                flash_fx_timer = 1f;
        }

        void OnDeath()
        {
            animator.SetTrigger("Death");
        }
    }

}