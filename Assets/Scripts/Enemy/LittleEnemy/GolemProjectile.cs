using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
    public float moveSpeed;
    public int damage;
    private Transform player;
    private Rigidbody2D playerDirection;
    private Vector2 target;
    private Vector2 originalPosition;
    public float shootingDistance;
    public Transform detection;
    private float ground_distance = 0.2f;
    private float wall_distance = 0.2f;
    
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
        if (littleEnemy.GetBeAttackedStatus())
        {
            ProjectileDestroy();
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
        FlipProjectile();

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }

        if(Vector2.Distance(transform.position, originalPosition) > shootingDistance)
        {
            Destroy(gameObject);
        }

        int layer_mask_ground = LayerMask.GetMask("Ground");
        int layer_mask_wall = LayerMask.GetMask("Wall");
        RaycastHit2D groundInfo_right = Physics2D.Raycast(detection.position, Vector2.right, ground_distance, layer_mask_ground);
        RaycastHit2D groundInfo_left = Physics2D.Raycast(detection.position, Vector2.left, ground_distance, layer_mask_ground);
        RaycastHit2D wallInfo_right = Physics2D.Raycast(detection.position, Vector2.right, wall_distance, layer_mask_wall);
        RaycastHit2D wallInfo_left = Physics2D.Raycast(detection.position, Vector2.left, wall_distance, layer_mask_wall);
        if(groundInfo_right.collider == true || groundInfo_left.collider == true || wallInfo_right == true || wallInfo_left == true)
        {
            Destroy(gameObject);
        }
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
    }

    public void ProjectileDestroy()
    {
        Destroy(gameObject);
        Debug.Log("Hit the bullets!!!!");
    }

    void FlipProjectile()
    {
        if(target.x > gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(target.x < gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
