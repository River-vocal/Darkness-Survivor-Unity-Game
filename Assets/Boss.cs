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
