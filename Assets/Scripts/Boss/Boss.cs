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

    void Start()
    {
        curHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        bossIsFlipped = false;

        //Track data of bossdata
        
        //Initial states
        GlobalAnalysis.boss_initail_healthpoints = curHealth;
        GlobalAnalysis.level = "1";
        StartInfo si = new StartInfo("1", GlobalAnalysis.getTimeStamp());
        AnalysisSender.Instance.postRequest("start", JsonUtility.ToJson(si));
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
            //Analysis Data
            GlobalAnalysis.state = "boss_dead";
            AnalysisSender.Instance.postRequest("play_info", GlobalAnalysis.buildPlayInfoData());
            GlobalAnalysis.cleanData();

            int currLevelIndex = SceneManager.GetActiveScene().buildIndex;
            if (currLevelIndex == 4)
            {
                Invoke("restart", 1f);
            }
            else
            {
                Invoke("goNextLevel", 1f);
            }
        }
        if(damage>10){
            DamagePopupManager.Create(damage, transform.position, 3);
        }else{
            DamagePopupManager.Create(damage, transform.position, 2);
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

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void goNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
