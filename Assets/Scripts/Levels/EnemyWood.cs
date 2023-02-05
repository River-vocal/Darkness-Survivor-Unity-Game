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
        // bossIsFlipped = false;
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        
    }

    private void health_OnDead(object sender, System.EventArgs e)
    {
        // GlobalAnalysis.is_boss_killed = true;
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
