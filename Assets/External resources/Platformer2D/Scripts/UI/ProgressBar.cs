using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IndieMarc.Platformer
{
    public class ProgressBar : MonoBehaviour
    {
        public float value = 0f;
        public float value_max = 100f;

        public Image fill;
        public Text amount;

        void Start()
        {

        }

        void Update()
        {
            if (fill)
                fill.fillAmount = value / value_max;
            if (amount)
                amount.text = Mathf.RoundToInt(value).ToString();
        }

    }

}