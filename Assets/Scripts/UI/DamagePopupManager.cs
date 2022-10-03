using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    static Color[] colors = {
        new Color(0.3f, 0.3f, 0.1f, 1), // player
        new Color(0.5f, 0.1f, 0.1f, 1), // player critical
        new Color(0.4f, 0.3f, 0.1f, 1), // enemy
        new Color(0.8f, 0.1f, 0.1f, 1)  // enemy critical
    };

    const int normalFontSize = 12;
    const int criticalFontSize = 16;

    private static DamagePopup createPopup(Vector3 position){
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        return damagePopup;
    }

    public static void Create(int damage, Vector3 position, int flag){
        int fontSize = (flag%2)==1 ? criticalFontSize : normalFontSize;
        Color color = colors[flag];
        DamagePopup damagePopup = createPopup(position);
        damagePopup.Setup(damage, color, fontSize);
    }

}
