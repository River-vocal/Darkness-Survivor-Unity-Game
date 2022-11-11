using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Platformer character movement
/// Author: Indie Marc (Marc-Antoine Desbiens)
/// </summary>

namespace IndieMarc.Platformer
{
    public enum PlayerCharacterState
    {
        Normal=0,
        Climb = 10,
        Dead=50,
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerCharacter : MonoBehaviour
    {
        public int player_id;

        [Header("Stats")]
        public float max_hp = 100f;
        
        [Header("Movement")]
        public float move_accel = 20f;
        public float move_deccel = 20f;
        public float move_max = 5f;

        [Header("Jump")]
        public bool can_jump = true;
        public bool double_jump = true;
        //public bool jump_on_enemies = true;
        public float jump_strength = 5f;
        public float jump_time_min = 0.4f;
        public float jump_time_max = 0.8f;
        public float jump_gravity = 5f;
        public float jump_fall_gravity = 10f;
        public float jump_move_percent = 0.75f;
        public LayerMask raycast_mask;
        public float ground_raycast_dist = 0.1f;

        [Header("Crouch")]
        public bool can_crouch = true;
        public float crouch_coll_percent = 0.5f;

        [Header("Climb")]
        public float climb_speed = 2f;

        [Header("Fall Below Level")]
        public bool reset_when_fall = true;
        public float fall_pos_y = -5f;
        public float fall_damage = 1f;

        [Header("Audio")]
        public AudioClip jump_audio;
        public AudioClip damage_audio;
        public AudioClip heal_audio;
        public AudioClip death_audio;

        public UnityAction onDeath;
        public UnityAction onHit;
        public UnityAction onJump;
        public UnityAction onLand;
        public UnityAction onCrouch;
        public UnityAction onClimb;

        private Rigidbody2D rigid;
        private CapsuleCollider2D capsule_coll;
        private ContactFilter2D contact_filter;
        private Vector2 coll_start_h;
        private Vector2 coll_start_off;
        private Vector3 start_scale;
        private Vector3 last_ground_pos;
        //private Vector3 average_ground_pos;

        private PlayerCharacterState state;
        private Vector2 move;

        private float hp;
        private bool was_grounded = false;
        private bool is_grounded = false;
        private bool is_fronted = false;
        private bool is_ceiled = false;
        private bool is_crouch = false;
        private bool is_jumping = false;
        private bool is_double_jump = false;
        private Vector3 grab_pos;

        private float state_timer = 0f;
        private float jump_timer = 0f;
        private float hit_timer = 0f;
        //private float grounded_timer = 0f;

        private static List<PlayerCharacter> character_list = new List<PlayerCharacter>();

        void Awake()
        {
            character_list.Add(this);
            rigid = GetComponent<Rigidbody2D>();
            capsule_coll = GetComponent<CapsuleCollider2D>();
            coll_start_h = capsule_coll.size;
            coll_start_off = capsule_coll.offset;
            start_scale = transform.localScale;
            //average_ground_pos = transform.position;
            last_ground_pos = transform.position;
            hp = max_hp;

            contact_filter = new ContactFilter2D();
            contact_filter.layerMask = raycast_mask;
            contact_filter.useLayerMask = true;
            contact_filter.useTriggers = false;

        }

        void OnDestroy()
        {
            character_list.Remove(this);
        }

        void Start()
        {
            PlayerData pdata = PlayerData.Get();
            if (pdata != null)
            {
                if (pdata.hp > 0)
                    hp = pdata.hp;

                pdata.hp = hp;
            }
        }

        //Handle physics
        void FixedUpdate()
        {
            if (TheGame.IsGamePaused())
                return;

            PlayerControls controls = PlayerControls.Get(player_id);

            //Movement velocity
            Vector3 move_input = controls.GetMove();
            float desiredSpeed = Mathf.Abs(move_input.x) > 0.1f ? move_input.x * move_max : 0f;
            float acceleration = Mathf.Abs(move_input.x) > 0.1f ? move_accel : move_deccel;
            acceleration = !is_grounded ? jump_move_percent * acceleration : acceleration;
            move.x = Mathf.MoveTowards(move.x, desiredSpeed, acceleration * Time.fixedDeltaTime);

            was_grounded = is_grounded;
            is_grounded = DetectObstacle(Vector3.down);
            is_ceiled = DetectObstacle(Vector3.up);
            is_fronted = IsFronted();

            if (state == PlayerCharacterState.Normal)
            {
                UpdateFacing();
                UpdateJump();
                UpdateCrouch();

                //Move
                move.x = is_fronted ? 0f : move.x;
                rigid.velocity = move;

                CheckForFloorTrigger();
            }

            if (state == PlayerCharacterState.Climb)
            {
                move = controls.GetMove() * climb_speed;
                rigid.velocity = move;
            }

            if (state == PlayerCharacterState.Dead)
            {
                move.x = 0f;
                UpdateJump(); //Keep falling
                rigid.velocity = move;
            }
        }

        //Handle render and controls
        void Update()
        {
            if (TheGame.IsGamePaused())
                return;

            hit_timer += Time.deltaTime;
            state_timer += Time.deltaTime;
            //grounded_timer += Time.deltaTime;

            //Controls
            PlayerControls controls = PlayerControls.Get(player_id);

            if (state == PlayerCharacterState.Normal)
            {
                if (controls.GetJumpDown())
                    Jump();

                Ladder ladder = Ladder.GetOverlapLadder(gameObject);
                if (ladder && controls.GetMove().y > 0.1f && state_timer > 0.7f)
                    Climb();

                
            }

            if (state == PlayerCharacterState.Climb)
            {
                Ladder ladder = Ladder.GetOverlapLadder(gameObject);
                if (ladder == null)
                {
                    state = PlayerCharacterState.Normal;
                    state_timer = 0f;
                }
                
                if (controls.GetJumpDown())
                    Jump(true);
            }
            
            //Reset when fall
            if (!IsDead() && transform.position.y < fall_pos_y - GetSize().y)
            {
                TakeDamage(fall_damage);
                if (reset_when_fall)
                    Teleport(last_ground_pos);
            }
            
        }

        private void UpdateFacing()
        {
            if (Mathf.Abs(move.x) > 0.01f)
            {
                float side = (move.x < 0f) ? -1f : 1f;
                transform.localScale = new Vector3(start_scale.x * side, start_scale.y, start_scale.z);
            }
        }

        private void UpdateJump()
        {
            PlayerControls controls = PlayerControls.Get(player_id);

            //Jump
            jump_timer += Time.fixedDeltaTime;

            //Jump end timer
            if (is_jumping && !controls.GetJumpHold() && jump_timer > jump_time_min)
                is_jumping = false;
            if (is_jumping && jump_timer > jump_time_max)
                is_jumping = false;

            //Jump hit ceil
            if (is_ceiled)
            {
                is_jumping = false;
                move.y = Mathf.Min(move.y, 0f);
            }

            //Add jump velocity
            if (!is_grounded)
            {
                //Falling
                float gravity = !is_jumping ? jump_fall_gravity : jump_gravity; //Gravity increased when going down
                move.y = Mathf.MoveTowards(move.y, -move_max * 2f, gravity * Time.fixedDeltaTime);
            }
            else if (!is_jumping)
            {
                //Grounded
                move.y = 0f;
            }

            /*if (!is_grounded)
                grounded_timer = 0f;

            //Average grounded pos
            if (!was_grounded && is_grounded)
                average_ground_pos = transform.position;
            if (is_grounded)
                average_ground_pos = Vector3.Lerp(transform.position, average_ground_pos, 1f * Time.deltaTime);

            //Save last landed position
            if (is_grounded && grounded_timer > 1f)
                last_ground_pos = average_ground_pos;*/

            if (!was_grounded && is_grounded)
            {
                if (onLand != null)
                    onLand.Invoke();
            }
        }

        private void UpdateCrouch()
        {
            if (!can_crouch)
                return;

            PlayerControls controls = PlayerControls.Get(player_id);

            //Crouch
            bool was_crouch = is_crouch;
            if (controls.GetMove().y < -0.1f && is_grounded)
            {
                is_crouch = true;
                move = Vector2.zero;
                capsule_coll.size = new Vector2(coll_start_h.x, coll_start_h.y * crouch_coll_percent);
                capsule_coll.offset = new Vector2(coll_start_off.x, coll_start_off.y - coll_start_h.y * (1f - crouch_coll_percent) / 2f);

                if (!was_crouch && is_crouch)
                {
                    if (onCrouch != null)
                        onCrouch.Invoke();
                }
            }
            else
            {
                is_crouch = false;
                capsule_coll.size = coll_start_h;
                capsule_coll.offset = coll_start_off;
            }
        }

        public void Jump(bool force_jump = false)
        {
            if (can_jump && (!is_crouch || force_jump))
            {
                if (is_grounded || force_jump || (!is_double_jump && double_jump))
                {
                    is_double_jump = !is_grounded;
                    move.y = jump_strength;
                    jump_timer = 0f;
                    is_jumping = true;
                    state = PlayerCharacterState.Normal;
                    state_timer = 0f;
                    if (onJump != null)
                        onJump.Invoke();

                    TheAudio.Instance.PlaySound("player", jump_audio);
                }
            }
        }

        public void Climb()
        {
            state = PlayerCharacterState.Climb;
            state_timer = 0f;

            if (onClimb != null)
                onClimb.Invoke();
        }

        private void CheckForFloorTrigger()
        {
            //Platform
            Vector3 center = GetCapsulePos(Vector3.down);
            float radius = GetCapsuleRadius() + ground_raycast_dist;
            GameObject platform = RaycastObstacle<PlatformMoving>(center, Vector3.down * radius);
            if (platform && platform.GetComponent<PlatformMoving>())
            {
                PlatformMoving pmoving = platform.GetComponent<PlatformMoving>();
                pmoving.OnCharacterStep();
                rigid.position += new Vector2( pmoving.GetMove().x, pmoving.GetMove().y) * Time.fixedDeltaTime;
            }

            //Enemy
            GameObject enemy_trigger = RaycastObstacle<Enemy>(center, Vector3.down * radius);
            if (enemy_trigger && enemy_trigger.GetComponent<Enemy>())
            {
                Enemy etrigger = enemy_trigger.GetComponent<Enemy>();
                TouchEnemy(etrigger);
            }

            //Floor trigger
            GameObject floor_trigger = RaycastObstacle<FloorTrigger>(center, Vector3.down * radius);
            if (floor_trigger && floor_trigger.GetComponent<FloorTrigger>())
            {
                FloorTrigger ftrigger = floor_trigger.GetComponent<FloorTrigger>();
                ftrigger.Activate();
            }
        }

        private bool IsFronted()
        {
            bool obstacle = DetectObstacle(GetFacing());
            bool box = RaycastObstacle<Box>(GetCapsulePos(GetFacing()), GetFacing());
            bool enemy = RaycastObstacle<Enemy>(GetCapsulePos(GetFacing()), GetFacing());
            return obstacle && !box && !enemy;
        }

        private bool DetectObstacle(Vector3 dir)
        {
            bool grounded = false;
            Vector2[] raycastPositions = new Vector2[3];

            Vector2 raycast_start = rigid.position;
            Vector2 orientation = dir.normalized;
            bool is_up_down = Mathf.Abs(orientation.y) > Mathf.Abs(orientation.x);
            Vector2 perp_ori = is_up_down ? Vector2.right : Vector2.up;
            float radius = GetCapsuleRadius();

            if (capsule_coll != null && is_up_down)
            {
                //Adapt raycast to collider
                raycast_start = GetCapsulePos(dir);
            }

            float ray_size = radius + ground_raycast_dist;
            raycastPositions[0] = raycast_start - perp_ori * radius / 2f;
            raycastPositions[1] = raycast_start;
            raycastPositions[2] = raycast_start + perp_ori * radius / 2f;
            
            for (int i = 0; i < raycastPositions.Length; i++)
            {
                Debug.DrawRay(raycastPositions[i], orientation * ray_size);
                if (RaycastObstacle(raycastPositions[i], orientation * ray_size))
                    grounded = true;
            }
            return grounded;
        }

        public bool RaycastObstacle(Vector2 pos, Vector2 dir)
        {
            RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
            Physics2D.Raycast(pos, dir.normalized, contact_filter, hitBuffer, dir.magnitude);
            for (int j = 0; j < hitBuffer.Length; j++)
            {
                if (hitBuffer[j].collider != null && hitBuffer[j].collider != capsule_coll && !hitBuffer[j].collider.isTrigger)
                {
                    return true;
                }
            }
            return false;
        }

        public GameObject RaycastObstacle<T>(Vector2 pos, Vector2 dir)
        {
            RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
            Physics2D.Raycast(pos, dir.normalized, contact_filter, hitBuffer, dir.magnitude);
            for (int j = 0; j < hitBuffer.Length; j++)
            {
                if (hitBuffer[j].collider != null && hitBuffer[j].collider != capsule_coll && !hitBuffer[j].collider.isTrigger)
                {
                    if (hitBuffer[j].collider.GetComponent<T>() != null)
                        return hitBuffer[j].collider.gameObject;
                }
            }
            return null;
        }

        public void Teleport(Vector3 pos)
        {
            transform.position = pos;
            last_ground_pos = pos;
            move = Vector2.zero;
            is_jumping = false;
        }

        public void SetRespawnPoint(Vector3 pos)
        {
            last_ground_pos = pos;
        }

        public void HealDamage(float heal)
        {
            if (!IsDead())
            {
                hp += heal;
                hp = Mathf.Min(hp, max_hp);

                TheAudio.Instance.PlaySound("player", heal_audio, 0.8f, true);
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsDead() && hit_timer > 0f)
            {
                hp -= damage;
                hit_timer = -1f;

                PlayerData pdata = PlayerData.Get();
                if(pdata != null)
                    pdata.hp = hp;

                if (hp <= 0f)
                {
                    Kill();
                }
                else
                {
                    TheAudio.Instance.PlaySound("player", damage_audio);

                    if (onHit != null)
                        onHit.Invoke();
                }
            }
        }

        public void Kill()
        {
            if (!IsDead())
            {
                state = PlayerCharacterState.Dead;
                rigid.velocity = Vector2.zero;
                move = Vector2.zero;
                state_timer = 0f;
                capsule_coll.enabled = false;

                TheAudio.Instance.PlaySound("player", death_audio, 0.8f, true);

                if (onDeath != null)
                    onDeath.Invoke();
            }
        }
        
        public PlayerCharacterState GetState() {
            return state;
        }

        public Vector2 GetMove()
        {
            return move;
        }

        public Vector2 GetFacing()
        {
            return Vector2.right * Mathf.Sign(transform.localScale.x);
        }

        public bool IsJumping()
        {
            return is_jumping;
        }

        public bool IsGrounded()
        {
            return is_grounded;
        }

        public bool IsCrouching()
        {
            return is_crouch;
        }

        public bool IsClimbing()
        {
            return state == PlayerCharacterState.Climb;
        }

        public float GetHP()
        {
            return hp;
        }

        public bool IsDead()
        {
            return state == PlayerCharacterState.Dead;
        }

        public Vector2 GetSize()
        {
            if (capsule_coll != null)
                return new Vector2(Mathf.Abs(transform.localScale.x) * capsule_coll.size.x, Mathf.Abs(transform.localScale.y) * capsule_coll.size.y);
            return new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }

        private Vector3 GetCapsulePos(Vector3 dir)
        {
            Vector2 orientation = dir.normalized;
            Vector2 raycast_offset = capsule_coll.offset + orientation * Mathf.Abs(capsule_coll.size.y * 0.5f - capsule_coll.size.x * 0.5f);
            return rigid.position + raycast_offset * capsule_coll.transform.lossyScale.y;
        }

        private float GetCapsuleRadius()
        {
            return GetSize().x * 0.5f;
        }

        private void TouchEnemy(Enemy enemy)
        {
            Vector3 diff = GetCapsulePos(Vector3.down) - enemy.transform.position;
            if (diff.y > 0f) {
                Jump(true); //Bounce on enemy
                enemy.Kill(); //Kill enemy
            }
            else {
                TakeDamage(enemy.damage);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsDead())
                return;

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                TouchEnemy(enemy);
            }
        }

        public static PlayerCharacter GetNearest(Vector3 pos, float range = 99999f, bool alive_only=false)
        {
            PlayerCharacter nearest = null;
            float min_dist = range;
            foreach (PlayerCharacter character in GetAll())
            {
                if (!alive_only || !character.IsDead())
                {
                    float dist = (pos - character.transform.position).magnitude;
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        nearest = character;
                    }
                }
            }
            return nearest;
        }

        public static PlayerCharacter Get(int player_id)
        {
            foreach (PlayerCharacter character in GetAll())
            {
                if (character.player_id == player_id)
                {
                    return character;
                }
            }
            return null;
        }

        public static List<PlayerCharacter> GetAll()
        {
            return character_list;
        }
    }

}