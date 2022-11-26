using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : MonoBehaviour
{
    private LittleEnemy littleEnemy;
    public GameObject PiranhaBullet;
    public float startTimeBtwShots;
    public float StartShootingDistance;
    private float timeBtwShots;
    private float Distance2Player;
    private Transform player_transform;
    private bool Piranha_status = true;
    // private Golem golem;




    // Start is called before the first frame update
    private void Awake()
    {
        player_transform = GameObject.FindWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        // Collider2D = GetComponent<Collider2D>();
        // damage = littleEnemy.damage;
    }

    // Update is called once per frame
    void Update()
    {
        Distance2Player = Vector2.Distance(transform.position, player_transform.position);
        // timeBtwShots = golem.CreateProjectiles(timeBtwShots, Piranha_status, Distance2Player, StartShootingDistance, startTimeBtwShots);
        // if(timeBtwShots <= 0 && Piranha_status == true && Distance2Player < StartShootingDistance)
        // {
        // Instantiate(PiranhaBullet, transform.position, Quaternion.identity);
        // Debug.Log("Shot bullets!!!!!!!!!!!!!!!!");
        // timeBtwShots = startTimeBtwShots;
        // }
        // else
        // {
        // timeBtwShots -= Time.deltaTime;
        // }
    }

    private void CreateBullets()
    {
        Instantiate(PiranhaBullet, transform.position, Quaternion.identity);
    }
}
