using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float SHRINK_TIMER_MAX = 1f;
    private const float SHRINK_SPEED = 1f;

    [SerializeField] private Health health;

    private Image barImage;
    private Image shrinkBarImage;

    private float shrinkTimer;
    

    private void Awake() {
        barImage = transform.Find("bar").GetComponent<Image>();
        shrinkBarImage = transform.Find("shrinkBar").GetComponent<Image>();

        health.OnDamaged += health_OnDamaged;
        health.OnHealed += health_OnHealed;
    }

    private void Update() {
        if(barImage.fillAmount < shrinkBarImage.fillAmount){
            shrinkTimer -= Time.deltaTime;
            if (shrinkTimer < 0){
                shrinkBarImage.fillAmount -= SHRINK_SPEED * Time.deltaTime;
            }
        }
    }

    private void setValue(float normalizedHealth){
        barImage.fillAmount = normalizedHealth;
    }

    private void health_OnHealed(object sender, System.EventArgs e){
        shrinkBarImage.fillAmount = health.CurHealthNormalized;
        setValue(health.CurHealthNormalized);
    }

    private void health_OnDamaged(object sender, System.EventArgs e){
        shrinkTimer = SHRINK_TIMER_MAX;
        setValue(health.CurHealthNormalized);
    }

}
