using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public float speed;
    public float StartShootingDistance;
    private float ground_distance = 1f;
    private float wall_distance = 0.2f;
    private bool movingRight = true;
    public Transform groundDetection;
    // public Transform wallDetection;
    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Animator golem_animation;
    private bool Golem_status = true;
    private Transform player_transform;
    public VisualEffectSystemManager VisualEffect;

    private void Awake()
    {
        golem_animation = GetComponent<Animator>();
        player_transform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(timeBtwShots <= 0 && Golem_status == true && Vector2.Distance(transform.position, player_transform.position) < StartShootingDistance)
        {
        Instantiate(projectile, transform.position, Quaternion.identity);
        timeBtwShots = startTimeBtwShots;
        }
        else
        {
        timeBtwShots -= Time.deltaTime;
        }               
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            golem_animation.SetBool("Attack 0", true);
            

            GameObject player = other.gameObject;
            if (other.GetComponent<Energy>().CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
            other.GetComponent<Energy>().CurEnergy -= damage;
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
