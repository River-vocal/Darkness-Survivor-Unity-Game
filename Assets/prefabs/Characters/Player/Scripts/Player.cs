using System.Collections;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerKouchokuState KouchokuState { get; private set; }
    public PlayerRangeAttackState RangeAttackState { get; private set; }

    #endregion

    #region Components

    public Rigidbody2D RigidBody;
    public Animator Animator { get; private set; }
    public CinemachineImpulseSource cinemachineImpulseSource;
    private Renderer renderer;
    public VisualEffectSystemManager VisualEffectSystemManager;
    public PlayerInputHandler InputHandler { get; private set; }
    
    public GameObject BulletPickupPrefab;
    
    [SerializeField] private PlayerData playerData;
    public GameObject DashBlue;
    public GameObject SwordAttackVFX;
    public ParallaxController ParallaxController;
    public SoundManager SoundManager;
    public psAttackManager attackManager;

    #endregion

    #region Runtime Variables

    public Vector2 CurVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public bool Invulnerable { get; private set; }
    private Energy energy;
    private float dragHolder;
    private float gravityScaleHolder;

    private Vector2 velocityHolder;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] public Transform attackCheck;
    [SerializeField] public int playerBulletCount;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        //use same animation Trigger since we use blend for in air animation
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "wallJump");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimb");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        KouchokuState = new PlayerKouchokuState(this, StateMachine, playerData, "kouchoku");
        RangeAttackState = new PlayerRangeAttackState(this, StateMachine, playerData, "rangeAttack");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RigidBody = GetComponent<Rigidbody2D>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        renderer = GetComponent<Renderer>();
        energy = GetComponent<Energy>();
        FacingDirection = 1;
        Invulnerable = false;
        StateMachine.Init(IdleState);
        playerBulletCount = playerData.playerInitialBulletCount;
    }

    private void Update()
    {
        CurVelocity = RigidBody.velocity;
        StateMachine.CurState.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.CurState.FixedUpdate();
    }

    #endregion

    #region Setters

    public void SetXVelocity(float v)
    {
        velocityHolder.Set(v, CurVelocity.y);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }

    public void SetYVelocity(float v)
    {
        velocityHolder.Set(CurVelocity.x, v);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }

    public void SetVelocity(float v, Vector2 direction)
    {
        direction.Normalize();
        velocityHolder.Set(v * direction.x, v * direction.y);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }

    public void SetVelocity(Vector2 v)
    {
        velocityHolder.Set(v.x, v.y);
        RigidBody.velocity = velocityHolder;
        CurVelocity = velocityHolder;
    }

    public void SetDrag(float drag)
    {
        dragHolder = RigidBody.drag;
        RigidBody.drag = drag;
    }

    public void SetGravityScale(float gravityScale)
    {
        gravityScaleHolder = RigidBody.gravityScale;
        RigidBody.gravityScale = gravityScale;
    }

    public void ResetDrag()
    {
        RigidBody.drag = dragHolder;
    }

    public void ResetGravityScale()
    {
        RigidBody.gravityScale = gravityScaleHolder;
    }
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
    
    public void AnimationFinished() => StateMachine.CurState.AnimationFinished();

    #endregion

    #region Checkers

    public void CheckIfShouldFlip(int x)
    {
        if (x != 0 && x != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckDistance, playerData.groundLayer);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.wallLayer);
    }

    public bool CheckIfBackTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.left * FacingDirection, playerData.backWallCheckDistance, playerData.wallLayer);
    }
    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckDistance);
        Gizmos.DrawWireSphere(wallCheck.position, playerData.wallCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, playerData.attackCheckDistance);
    }
    #endregion

    #region Other

    public void TakeDamage(float damage, params Object[] args)
    {
        if (!Invulnerable)
        {
            energy.CurEnergy -= damage;
            SoundManager.PlaySound("injured");
            StartCoroutine(Blink());
            StateMachine.ChangeState(KouchokuState, args);
        }
    }
    
    private IEnumerator Blink()
    {
        float timeElapsed = 0;
        Invulnerable = true;
        var holder = renderer.material.color;
        renderer.material.color = Color.red;
        
        while (timeElapsed < playerData.invulnerableTime)
        {
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
            }

            timeElapsed += playerData.blinkInterval;
            yield return new WaitForSeconds(playerData.blinkInterval);
        }
        renderer.enabled = true;
        Invulnerable = false;
        renderer.material.color = holder;
    }

    public void PlayAttackSound()
    {
        SoundManager.PlaySound("attack");
    }

    public void PlayRangeAttackSound()
    {
        SoundManager.PlaySound("rangeAttack");
    }

    public void EnableSwordAttackVFX()
    {
        SwordAttackVFX.SetActive(true);
    }

    public void DealDamageTo(Collider2D collider)
    {
        cinemachineImpulseSource.GenerateImpulse();
        VisualEffectSystemManager.GenerateExplosionNovaFire(collider.transform);
        ParallaxController.StopFollowing();

        if (collider.tag == "Drop")
        {
            Instantiate(BulletPickupPrefab, collider.transform.position, collider.transform.rotation);
            collider.GetComponent<EnemyDrops>().DropDeath();
        }
        if (collider.tag == "Golem")
        {
            Instantiate(BulletPickupPrefab, collider.transform.position, collider.transform.rotation);
            collider.GetComponent<Golem>().GolemDeath();
        }
        if (collider.tag == "Projectile")
        {
            Instantiate(BulletPickupPrefab, collider.transform.position, collider.transform.rotation);
            collider.GetComponent<GolemProjectile>().ProjectileDestroy();
        }
        else
        {
            Health health = collider.GetComponent<Health>();
            if (health) health.CurHealth -= playerData.attackDamage;
        }
    }
    #endregion
}
