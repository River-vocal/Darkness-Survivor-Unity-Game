using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    public DamageText damageText;
    public GameObject canvas;

    public void CreateDamagePopup(int damage, Transform location){
        DamageText instance = Instantiate(damageText, location);
        Vector2 screenPostion = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(""+damage);
        instance.SetLocation(screenPostion);
    }

}
