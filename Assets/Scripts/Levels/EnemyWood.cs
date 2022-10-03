using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWood : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    private int curHealth;
    [SerializeField] private HealthBar healthBar;
    // public bool bossIsFlipped;
    // Start is called before the first frame update

    /*
    [SerializeField] private SwordAttack swordAttack;

    void attack() {
        if (bossIsFlipped) {
            swordAttack.AttackLeft();
        }
        else swordAttack.AttackRight();
    }

    void stopAttack() {
        swordAttack.StopAttack();
    }
    */

    public Datas tutorialdata = new Datas();
    void Start()
    {
        curHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        //Track data of tutorialdata
        
        //Initial states
        
        tutorialdata.level = "Tutorial";
        tutorialdata.num_players = 1;
        tutorialdata.num_bosses = 1;
        tutorialdata.state = "start";
        tutorialdata.timestamp = GlobalAnalysis.getTimeStamp();
        tutorialdata.player_remaining_healthpoints = 20;
        tutorialdata.boss_remaining_healthpoints = curHealth;
        string json = JsonUtility.ToJson(tutorialdata);

        StartCoroutine(GlobalAnalysis.postRequest("test", json));

        // bossIsFlipped = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        healthBar.setHealth(curHealth);

        if (curHealth <= 0)
        {
            // enemywood.state = "End";
            // enemywood.num_players = 1;
            // enemywood.timestamp = GlobalAnalysis.getTimeStamp();
            // enemywood.remaining_healthpoints = curHealth;
            // string json = JsonUtility.ToJson(enemywood);
            // StartCoroutine(GlobalAnalysis.postRequest("test", json));
            tutorialdata.level = "Tutorial";
            tutorialdata.num_players = 1;
            tutorialdata.num_bosses = 1;
            tutorialdata.state = "end";
            tutorialdata.timestamp = GlobalAnalysis.getTimeStamp();
            tutorialdata.player_remaining_healthpoints = 20;
            tutorialdata.boss_remaining_healthpoints = curHealth;
            string json = JsonUtility.ToJson(tutorialdata);

            StartCoroutine(GlobalAnalysis.postRequest("test", json));
            Invoke("LoadLevel1", 1f);
        }

        if(damage>10){
            DamagePopupManager.Create(damage, transform.position, 3);
        }else{
            DamagePopupManager.Create(damage, transform.position, 2);
        }
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
