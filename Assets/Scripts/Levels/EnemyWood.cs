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

        GlobalAnalysis.level = "0";
        GlobalAnalysis.boss_initail_healthpoints = health.MaxHealth;
        StartInfo si = new StartInfo("0", GlobalAnalysis.getTimeStamp());
        AnalysisSender.Instance.postRequest("start", JsonUtility.ToJson(si));

        // bossIsFlipped = false;
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        GlobalAnalysis.boss_remaining_healthpoints = health.CurHealth;
    }
    private void health_OnDead(object sender, System.EventArgs e)
    {
        GlobalAnalysis.state = "boss_dead";
        AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
        GlobalAnalysis.cleanData();
        Invoke("LoadLevel1", 1f);
    }
    void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }
}
