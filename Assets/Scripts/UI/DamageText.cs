using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textComponent;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    public void SetText(string text){
        textComponent.text = text;
    }

    public void SetLocation(Vector3 location){
        GetComponent<RectTransform>().position = location;
    }

}
