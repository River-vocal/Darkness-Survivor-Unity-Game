using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoBoss : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    private int curHealth;
    [SerializeField] private HealthBar healthBar;
    private GameObject healthBar1;
    private GameObject healthBar2;
    private GameObject boss1;
    private GameObject boss2;

    public bool bossIsFlipped;
    public int attackDamage = 10;
    // Start is called before the first frame update
    public Transform playerTransform;

    // public Datas bossdata = new Datas();

    void Start()
    {
        healthBar1 = GameObject.Find("Boss1/FloatingHealthBar/HealthBar");
        healthBar2 = GameObject.Find("Boss2/FloatingHealthBar/HealthBar");
        boss1 = GameObject.Find("Boss1");
        boss2 = GameObject.Find("Boss2");

        curHealth = maxHealth;
        // healthBar.setValue(maxHealth);
        bossIsFlipped = false;

        //Track data of bossdata
        
        //Initial states
        GlobalAnalysis.boss_remaining_healthpoints = curHealth;
        // bossdata.level = "1";
        // bossdata.num_players = 1;
        // bossdata.num_bosses = 1;
        // bossdata.state = "start";
        // bossdata.timestamp = GlobalAnalysis.getTimeStamp();
        // bossdata.player_remaining_healthpoints = GlobalAnalysis.player_remaining_healthpoints;
        // bossdata.boss_remaining_healthpoints = curHealth;
        // string json = JsonUtility.ToJson(bossdata);

        // StartCoroutine(GlobalAnalysis.postRequest("test", json));
    }

    // Update is called once per frame
    void Update()
    {
    	if (name == "Boss1") {
    		if (GetComponent<Health>().CurHealth <= 0) {
    			boss1.SetActive(false);
    			if (boss2 == null || !boss2.activeSelf) {
    				Invoke("Restart", 1f);
    			}
    		}
    	} else if (name == "Boss2") {
    		if (GetComponent<Health>().CurHealth <= 0) {
    			boss2.SetActive(false);
    			if (boss1 == null || !boss1.activeSelf) {
    				Invoke("Restart", 1f);
    			}
    		}
    	}
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        // healthBar.setHealth(curHealth);
        GlobalAnalysis.boss_remaining_healthpoints = curHealth;

        if (curHealth <= 0)
        {
            if (name == "Boss1") {
                boss1.SetActive(false);
                if (healthBar2.GetComponent<Slider>().value <= 0) {
                    // bossdata.level = "1";
                    // bossdata.num_players = 1;
                    // bossdata.num_bosses = 1;
                    // bossdata.state = "end";
                    // bossdata.timestamp = GlobalAnalysis.getTimeStamp();
                    // bossdata.player_remaining_healthpoints = GlobalAnalysis.player_remaining_healthpoints;
                    // bossdata.boss_remaining_healthpoints = curHealth;
                    // string json = JsonUtility.ToJson(bossdata);

                    // StartCoroutine(GlobalAnalysis.postRequest("test", json));
                    Invoke("Restart", 1f);
                }
            }
            else if (name == "Boss2") {
                boss2.SetActive(false);
                if (healthBar1.GetComponent<Slider>().value <= 0) {
                    // bossdata.level = "1";
                    // bossdata.num_players = 1;
                    // bossdata.num_bosses = 1;
                    // bossdata.state = "end";
                    // bossdata.timestamp = GlobalAnalysis.getTimeStamp();
                    // bossdata.player_remaining_healthpoints = GlobalAnalysis.player_remaining_healthpoints;
                    // bossdata.boss_remaining_healthpoints = curHealth;
                    // string json = JsonUtility.ToJson(bossdata);

                    // StartCoroutine(GlobalAnalysis.postRequest("test", json));
                    Invoke("Restart", 1f);
                }
            }
        }

        // if(damage>10){
        //     DamagePopupManager.Create(damage, transform.position, 3);
        // }else{
        //     DamagePopupManager.Create(damage, transform.position, 2);
        // }
    }
    
    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;
        if (transform.position.x > playerTransform.position.x && bossIsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            bossIsFlipped = false;
        }
        else if (transform.position.x < playerTransform.position.x && !bossIsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            bossIsFlipped = true;
        }
    }
    
    public void DirectionChange()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;
        if (bossIsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            bossIsFlipped = false;
        }
        else if (!bossIsFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            bossIsFlipped = true;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
