using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public int lives_num;
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
        Debug.Log("Dead!!!!!!!!!!!!!!!!!!!!!!");
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
