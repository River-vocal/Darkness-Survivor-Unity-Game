using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    private int curHealth;
    [SerializeField] private HealthBar healthBar;
    public bool bossIsFlipped;
    public int attackDamage = 10;
    // Start is called before the first frame update
    public Transform playerTransform;

    public Datas bossdata = new Datas();

    void Start()
    {
        curHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        bossIsFlipped = false;

        //Track data of bossdata
        
        //Initial states
        GlobalAnalysis.boss_remaining_healthpoints = curHealth;
        bossdata.level = "1";
        bossdata.num_players = 1;
        bossdata.num_bosses = 1;
        bossdata.state = "start";
        bossdata.timestamp = GlobalAnalysis.getTimeStamp();
        bossdata.player_remaining_healthpoints = GlobalAnalysis.player_remaining_healthpoints;
        bossdata.boss_remaining_healthpoints = curHealth;
        string json = JsonUtility.ToJson(bossdata);

        StartCoroutine(GlobalAnalysis.postRequest("test", json));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        healthBar.setHealth(curHealth);
        GlobalAnalysis.boss_remaining_healthpoints = curHealth;

        if (curHealth <= 0)
        {
            
            bossdata.level = "1";
            bossdata.num_players = 1;
            bossdata.num_bosses = 1;
            bossdata.state = "end";
            bossdata.timestamp = GlobalAnalysis.getTimeStamp();
            bossdata.player_remaining_healthpoints = GlobalAnalysis.player_remaining_healthpoints;
            bossdata.boss_remaining_healthpoints = curHealth;
            string json = JsonUtility.ToJson(bossdata);

            // StartCoroutine(GlobalAnalysis.postRequest("test", json));
            Invoke("Restart", 1f);
        }
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

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
