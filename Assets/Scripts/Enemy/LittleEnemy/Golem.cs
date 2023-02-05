using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
    
    public Transform groundDetection;
    public GameObject projectile;
    public VisualEffectSystemManager VisualEffect;
    
    private int damage;
    private float speed;
    
    public float startTimeBtwShots;
    private Animator golem_animation;
    private Transform player_transform;
    private Collider2D Collider2d;

    private bool Golem_status = true;
    public float StartShootingDistance;
    private float ground_distance = 1f;
    private float wall_distance = 0.2f;
    private bool movingRight = true;
    private float timeBtwShots;
    private float Distance2Player;
    
    private void Awake()
    {
        golem_animation = GetComponent<Animator>();
        player_transform = GameObject.FindWithTag("Player").transform;
        Collider2d = GetComponent<Collider2D>();
        damage = littleEnemy.damage;
        speed = littleEnemy.speed;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (littleEnemy.GetBeAttackedStatus())
        {
            GolemDeath();
            littleEnemy.SetBeAttackedStatus(false);
            littleEnemy.SetDeathStatus(true);
            return;
        }
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        int layer_mask_ground = LayerMask.GetMask("Ground");
        int layer_mask_wall = LayerMask.GetMask("Wall");
        RaycastHit2D groundInfo_down = Physics2D.Raycast(groundDetection.position, Vector2.down, ground_distance, layer_mask_ground);
        RaycastHit2D groundInfo_right = Physics2D.Raycast(groundDetection.position, Vector2.right, wall_distance, layer_mask_ground);
        RaycastHit2D groundInfo_left = Physics2D.Raycast(groundDetection.position, Vector2.left, wall_distance, layer_mask_ground);
        RaycastHit2D wallInfo_right = Physics2D.Raycast(groundDetection.position, Vector2.right, wall_distance, layer_mask_wall);
        RaycastHit2D wallInfo_left = Physics2D.Raycast(groundDetection.position, Vector2.left, wall_distance, layer_mask_wall);
        if(groundInfo_down.collider == false)
        {
            switchDirection();
        }
        else if(groundInfo_down.collider == true && wallInfo_right.collider == true)
        {
            switchDirection();
        }
        else if(groundInfo_down.collider == true && wallInfo_left.collider == true)
        {
            switchDirection();
        }
        else if(groundInfo_down.collider == true && groundInfo_right.collider == true)
        {
            switchDirection();
        }
        else if(groundInfo_down.collider == true && groundInfo_left.collider == true)
        {
            switchDirection();
        }
        Distance2Player = Vector2.Distance(transform.position, player_transform.position);
        timeBtwShots = CreateProjectiles(timeBtwShots, Golem_status, Distance2Player, StartShootingDistance, startTimeBtwShots);               
    }
    
    public float CreateProjectiles(float timeBtwShots, bool Golem_status, float Distance2Player, float StartShootingDistance, float startTimeBtwShots)
    {
        if(timeBtwShots <= 0 && Golem_status == true && Distance2Player < StartShootingDistance)
        {
        Instantiate(projectile, transform.position, Quaternion.identity);
        return timeBtwShots = startTimeBtwShots;
        }
        else
        {
        return timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            golem_animation.SetBool("Attack 0", true);
            

            GameObject player = other.gameObject;
            if (other.GetComponent<Energy>().CurEnergy < damage) {
                // GlobalAnalysis.player_status = "smallenemy_dead";
                // Debug.Log("lose by: small enemy");
            }
            // GlobalAnalysis.smallenemy_damage += damage;
            // other.GetComponent<Energy>().CurEnergy -= damage;
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            golem_animation.SetBool("Attack 0", false);
        }
    }

    public void GolemDeath()
    {
        golem_animation.SetBool("Attack 0", false);
        golem_animation.SetBool("GolemDeath", true);
        speed = 0;
        Golem_status = false;
        VisualEffect.GenerateEvilPurpleExplode(transform);
        Collider2d.enabled = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void switchDirection()
    {
        if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
    }    
}
