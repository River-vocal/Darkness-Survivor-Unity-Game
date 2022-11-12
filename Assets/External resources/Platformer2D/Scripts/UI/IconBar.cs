using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IndieMarc.Platformer
{
    public class IconBar : MonoBehaviour
    {
        public int value = 0;
        public int value_max = 4;

        public Image[] icons;
        public Sprite sprite_full;
        public Sprite sprite_empty;

        void Start()
        {

        }

        void Update()
        {
            int index = 0;
            foreach (Image icon in icons)
            {
                icon.gameObject.SetActive(index < value_max);
                icon.sprite = (index < value) ? sprite_full : sprite_empty;
                index++;
            }
        }
    }
}