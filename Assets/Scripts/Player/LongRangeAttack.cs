using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject playerBulletPrefab;
    public int playerBulletCount = 3;
    
    
    // Start is called before the first frame update
    // void Start()
    // {
    //    
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Pressed H!!!");
            
            if (playerBulletCount > 0)
            {
                PlayerShoot();
                playerBulletCount--;
            }

        }


    }

    void PlayerShoot()
    {
        Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);

    }

}
