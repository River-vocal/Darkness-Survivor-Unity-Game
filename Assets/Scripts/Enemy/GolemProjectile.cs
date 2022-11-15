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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (other.GetComponent<Energy>().CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
            other.GetComponent<Energy>().CurEnergy -= damage;
        }
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
