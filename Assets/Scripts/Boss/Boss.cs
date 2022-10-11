using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject allPassMenu;
    [SerializeField] private GameObject wonMenu;
    
    [SerializeField] private int maxHealth = 200;
    private int curHealth;
    [SerializeField] private HealthBar healthBar;
    public bool bossIsFlipped;
    public int attackDamage = 10;
    // Start is called before the first frame update
    public Transform playerTransform;

    private Health health;

    private void Awake() {
        health = GetComponent<Health>();
        health.OnDamaged += health_OnDamaged;
        health.OnDead += health_OnDead;
    }

    void Start()
    {
        bossIsFlipped = false;

        //Track data of bossdata
        
        //Initial states
        GlobalAnalysis.level = SceneManager.GetActiveScene().buildIndex.ToString();
        GlobalAnalysis.boss_initail_healthpoints = curHealth;
        GlobalAnalysis.start_time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); 
        GlobalAnalysis.scene = SceneManager.GetActiveScene().buildIndex.ToString();
        Debug.Log("Scene: "+ GlobalAnalysis.scene);
        StartInfo si = new StartInfo(GlobalAnalysis.level, GlobalAnalysis.getTimeStamp());
        AnalysisSender.Instance.postRequest("start", JsonUtility.ToJson(si));
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        int damage = ((IntegerEventArg) e).Value;
        GlobalAnalysis.boss_remaining_healthpoints = health.CurHealth; 

    }
    private void health_OnDead(object sender, System.EventArgs e)
    {
        //Analysis Data
        GlobalAnalysis.state = "boss_dead";
        AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
        GlobalAnalysis.cleanData();

        int currLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currLevelIndex == 5)
        {
                gameObject.SetActive(false);
            Invoke("GotoAllPassMenu", 1.5f);
        }
        else
        {
                gameObject.SetActive(false);
            Invoke("GotoWonMenu", 1.5f);
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

    void GotoAllPassMenu()
    {
        allPassMenu.SetActive(true);
        Time.timeScale = 0f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GotoWonMenu()
    {
        wonMenu.SetActive(true);
        Time.timeScale = 0f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
