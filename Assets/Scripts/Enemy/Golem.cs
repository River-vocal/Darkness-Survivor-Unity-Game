using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public float speed;
    private float distance = 2f;
    // [SerializeField] public int lives_num;
    private bool movingRight = true;
    public Transform groundDetection;
    // [SerializeField] GameObject projectile;
    private Animator golem_animation;
    private float width;
    private float height;
    private SpriteRenderer sp;
    // float fireRate;
    // float nextFire;

    private void Awake()
    {
        golem_animation = GetComponent<Animator>();
        width = GetComponent<Collider2D>().bounds.size.x;
        height = GetComponent<Collider2D>().bounds.size.y;
        sp = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    // void Start()
    // {
    //     fireRate = 1f;
    //     nextFire = Time.time;   
    // }

    // Update is called once per frame
    void Update()
    {
        // CheckIfTimeToFire();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        int layer_mask = LayerMask.GetMask ("Ground");
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, layer_mask);
        // bool groundInfo = Physics2D.OverlapCircle(groundDetection.position, distance, layer_mask);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                Debug.Log("Turn Left!!!!!!!!!!!!!!");
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                Debug.Log("Turn right!!!!!!!!!!!!!!");
                movingRight = true;
                
            }
        }


        
        
    }

    // void CheckIfTimeToFire()
    // {
    //     if (Time.time > nextFire)
    //     {
    //         Instantiate (projectile, transform.position, Quaternion.identity);
    //         nextFire = Time.time + fireRate;
    //     }
    // }

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
        	// if (player.transform.position.x >= transform.position.x)
            // {
            //     Flip();
	    	// 	Debug.Log("Flipped!!!!!!!!!!!!!!!!!");
	    	// }
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
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    

    // void Flip()
    // {
    //     transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    // }

    
}
