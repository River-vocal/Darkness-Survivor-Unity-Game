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

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Energy energy;
    [SerializeField] public float AnimationSpeed = 0.2f;

    private RawImage barRawImage;
    private float barMaskWidth => rectTransform.rect.width;
    private RectTransform rectTransform;
    private RectTransform barMaskRectTransform;
    private RectTransform edgeRectTransform;
    
    private void Awake()
    {
        rectTransform = (RectTransform) transform;
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (energy)
        {
            // Animation
            Rect uvRect = barRawImage.uvRect;
            uvRect.x += AnimationSpeed * Time.deltaTime;
            barRawImage.uvRect = uvRect;

            // Bar length
            Vector2 offsetMax = barMaskRectTransform.offsetMax;
            offsetMax.x = -((1f - energy.CurEnergyNormalized) * barMaskWidth);
            barMaskRectTransform.offsetMax = offsetMax;

            // Edge position
            edgeRectTransform.anchoredPosition = new Vector2(energy.CurEnergyNormalized * barMaskWidth, 0);
        }
    }

}


