using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoBoss : MonoBehaviour
{
    [SerializeField] private GameObject allPassMenu;
    [SerializeField] private GameObject wonMenu;

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
    }

    // Update is called once per frame
    void Update()
    {
    	if (name == "Boss1") {
    		if (GetComponent<Health>().CurHealth <= 0) {
    			boss1.SetActive(false);
    			if (boss2 == null || !boss2.activeSelf) {
    				gameObject.SetActive(false);
                    Invoke("GotoAllPassMenu", 1f);
    			}
    		}
    	} else if (name == "Boss2") {
    		if (GetComponent<Health>().CurHealth <= 0) {
    			boss2.SetActive(false);
    			if (boss1 == null || !boss1.activeSelf) {
    				gameObject.SetActive(false);
                    Invoke("GotoAllPassMenu", 1f);
    			}
    		}
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

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
