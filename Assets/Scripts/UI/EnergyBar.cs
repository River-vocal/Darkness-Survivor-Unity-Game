/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    [SerializeField] private Energy energy;
    private float barMaskWidth;
    private RectTransform barMaskRectTransform;
    private RectTransform edgeRectTransform;
    private RawImage barRawImage;

    private void Awake() {
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;
    }

    private void Update() {

        Rect uvRect = barRawImage.uvRect;
        uvRect.x += .2f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = energy.CurEnergyNormalized * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        edgeRectTransform.anchoredPosition = new Vector2(energy.CurEnergyNormalized * barMaskWidth, 0);

        edgeRectTransform.gameObject.SetActive(energy.CurEnergyNormalized < 1f);
    }

}


