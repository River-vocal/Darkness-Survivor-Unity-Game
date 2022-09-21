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
    // Start is called before the first frame update
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

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
