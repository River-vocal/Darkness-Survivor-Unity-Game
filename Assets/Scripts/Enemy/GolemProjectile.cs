using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    private Transform player;
    private Rigidbody2D playerDirection;
    private Vector2 target;
    private Vector2 originalPosition;
    public float shootingDistance;
    // public Transform detection;
    private float ground_distance = 0.8f;
    private float wall_distance = 0.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerDirection = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        
        FlipProjectile();

        if(transform.position.x == player.position.x && transform.position.y == player.position.y)
        {
            Destroy(gameObject);
        }

        if(Vector2.Distance(transform.position, originalPosition) > shootingDistance)
        {
            Destroy(gameObject);
        }

        // int layer_mask_ground = LayerMask.GetMask("Ground");
        // int layer_mask_wall = LayerMask.GetMask("Wall");
        // RaycastHit2D groundInfo_down = Physics2D.Raycast(detection.position, Vector2.down, ground_distance, layer_mask_ground);
        // RaycastHit2D groundInfo_up = Physics2D.Raycast(detection.position, Vector2.up, ground_distance, layer_mask_ground);
        // RaycastHit2D wallInfo_right = Physics2D.Raycast(detection.position, Vector2.right, wall_distance, layer_mask_wall);
        // RaycastHit2D wallInfo_left = Physics2D.Raycast(detection.position, Vector2.left, wall_distance, layer_mask_wall);
        // if(groundInfo_down.collider == false | groundInfo_up.collider == false | wallInfo_left.collider == false | wallInfo_right.collider == false)
        // {
        //     Destroy(gameObject);
        // }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.name == "Map")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            // Destroy(gameObject);
            if (other.GetComponent<Energy>().CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
            other.GetComponent<Energy>().CurEnergy -= damage;
        }
        // if(other.gameObject.name == "Map")
        // {
        //     Destroy(gameObject);
        // }
        
        // if(other.gameObject.layer == LayerMask.GetMask("Ground") | other.gameObject.layer == LayerMask.GetMask("Wall"))
        // {
        //     Debug.Log("Wall!!!!!!!!!!!!!!!!!!!!!!!!");
        //     Destroy(gameObject);
        // }
    }

    // void OnCollisionEnter2D(Collision2D collisionInfo)
    // {
    //     Destroy(gameObject);
    // }

    public void ProjectileDestroy()
    {
        Destroy(gameObject);
        Debug.Log("Hit the bullets!!!!");
    }

    void FlipProjectile()
    {
        if(player.position.x > gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(player.position.x < gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
