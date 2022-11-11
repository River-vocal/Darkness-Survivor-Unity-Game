using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class MeshSprite : MonoBehaviour
    {
        public string sort_layer = "Default";
        public int sort_order = 0;

        void Start()
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            mesh.sortingLayerName = sort_layer;
            mesh.sortingOrder = sort_order;
            enabled = false;
        }
        
    }
}