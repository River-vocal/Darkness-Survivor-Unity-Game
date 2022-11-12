using System;
using MyEventArgs;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public const float InitialBarLength = 300f;
    private Energy energy;
    [SerializeField] public float AnimationSpeed = 0.2f;

    private RawImage barRawImage;
    private float barMaskWidth => rectTransform.rect.width;
    private RectTransform rectTransform;
    private RectTransform barMaskRectTransform;
    private RectTransform edgeRectTransform;

    private float originAnimationSpeed;
    
    private void Awake()
    {
        energy = GameObject.FindWithTag("Player").GetComponent<Energy>();
        energy.OnDamageableChanged += Energy_OnDamageableChanged;
        rectTransform = (RectTransform) transform;
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();
        originAnimationSpeed = AnimationSpeed;
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

    private void Energy_OnDamageableChanged(object sender, System.EventArgs e)
    {
        bool damageable = ((BooleanEventArg)e).Value;
        AnimationSpeed = damageable ? originAnimationSpeed : -originAnimationSpeed;
    }

}


