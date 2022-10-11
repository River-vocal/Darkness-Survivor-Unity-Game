using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWood : MonoBehaviour
{
    private Health health;
    private int curHealth{
        get{
            return health.CurHealth;
        }
    }

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
        GlobalAnalysis.cleanData();

        // Tutorial level starts at "1";
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

    private void health_OnDead(object sender, System.EventArgs e)
    {
        GlobalAnalysis.state = "boss_dead";
        AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
        GlobalAnalysis.cleanData();
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
