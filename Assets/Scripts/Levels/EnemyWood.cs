using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWood : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDamaged += health_OnDamaged;
        health.OnDead += health_OnDead;
    }

    void Start()
    {
        //Track data of tutorialdata

        //Initial states

        // GlobalAnalysis.level = "0";
        GlobalAnalysis.level = SceneManager.GetActiveScene().buildIndex.ToString();
        GlobalAnalysis.boss_initail_healthpoints = curHealth;
        StartInfo si = new StartInfo(GlobalAnalysis.level, GlobalAnalysis.getTimeStamp());
        AnalysisSender.Instance.postRequest("start", JsonUtility.ToJson(si));

        // bossIsFlipped = false;
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        GlobalAnalysis.boss_remaining_healthpoints = health.CurHealth;
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
