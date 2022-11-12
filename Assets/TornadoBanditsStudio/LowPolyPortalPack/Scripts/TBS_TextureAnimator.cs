using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TornadoBanditsStudio
{
    public class TBS_TextureAnimator : MonoBehaviour
    {
        private float indexX = 0; //x index on texture
        private float indexY = 1; //y index on texture

        [SerializeField] private int uvTileX = 1; //number of x tiles
        [SerializeField] private int uvTileY = 1; //number of y tiles
        [SerializeField] private int fpsNumber = 10; //fps number
        [SerializeField] private bool isNormalMapAnimated = false; //if normal map animated

        private Vector2 size; //texture size
        private Renderer myRenderer; //refernece to renderer
        private int lastIndex = -1; //last index of animation

        void Start ()
        {
            //Set size based on number of tiles
            size = new Vector2 (1.0f / uvTileX,
                                1.0f / uvTileY);

            //Get renderer
            myRenderer = this.GetComponent<Renderer>();

            if (myRenderer == null)
                this.enabled = false;

            myRenderer.material.SetTextureScale ("_MainTex", size);

            if (isNormalMapAnimated)
                myRenderer.sharedMaterial.SetTextureScale ("_BumpMap", size);
        }



        void Update ()
        {
            int index = (int)(Time.timeSinceLevelLoad * fpsNumber) % (uvTileX * uvTileY);

            if (index != lastIndex)
            {
                Vector2 offset = new Vector2 (indexX * size.x,
                                              1 - (size.y * indexY));
                indexX++;
                if (indexX / uvTileX == 1)
                {
                    if (uvTileY != 1) indexY++;
                    indexX = 0;
                    if (indexY / uvTileY == 1)
                    {
                        indexY = 0;
                    }
                }

                myRenderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
                myRenderer.sharedMaterial.SetTextureOffset ("_BumpMap", offset);

                lastIndex = index;
            }
        }
    }
}
