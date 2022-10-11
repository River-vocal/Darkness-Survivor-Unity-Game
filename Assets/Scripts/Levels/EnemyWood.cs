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

    void Start()
    {
        curHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        //Track data of tutorialdata
        
        //Initial states

        // GlobalAnalysis.level = "0";
        GlobalAnalysis.level = SceneManager.GetActiveScene().buildIndex.ToString();
        GlobalAnalysis.boss_initail_healthpoints = curHealth;
        StartInfo si = new StartInfo(GlobalAnalysis.level, GlobalAnalysis.getTimeStamp());
        AnalysisSender.Instance.postRequest("start", JsonUtility.ToJson(si));

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
        GlobalAnalysis.boss_remaining_healthpoints = curHealth;
        if (curHealth <= 0)
        {
            GlobalAnalysis.state = "boss_dead";
            AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
            GlobalAnalysis.cleanData();
            Invoke("LoadNextLevel", 1f);
        }

        if(damage>10){
            DamagePopupManager.Create(damage, transform.position, 3);
        }else{
            DamagePopupManager.Create(damage, transform.position, 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
