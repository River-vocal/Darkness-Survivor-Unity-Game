using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveYSpeed = 2f;
    public float disappearDelay = 1f;
    public float disappearSpeed = 3f;

    private float disapperTimer = 1f;
    private TextMeshPro textMesh;
    private Color textColor;

    private void Awake() {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(String text, Color color, int fontSize){
        textMesh.SetText(text);
        textMesh.fontSize = fontSize;
        textColor = color;
        disapperTimer = disappearDelay;
        textMesh.color = textColor;
    }

    private void Update() {
        transform.position += new Vector3(0, moveYSpeed * Time.deltaTime);
        disapperTimer -= Time.deltaTime;

        if(disapperTimer<0){
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0){
                Destroy(gameObject);
            }
        }
    }   
    
    public static DamagePopup CreatePopup(Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        return damagePopup;
    }

}
