using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{

    public class Ladder : MonoBehaviour
    {

        private Collider2D collide;

        private static List<Ladder> ladder_list = new List<Ladder>();

        void Awake()
        {
            ladder_list.Add(this);
            collide = GetComponent<Collider2D>();
        }

        void OnDestroy()
        {
            ladder_list.Remove(this);
        }

        void Update()
        {

        }

        public static Ladder GetOverlapLadder(GameObject other)
        {
            foreach (Ladder ladder in ladder_list)
            {
                if (ladder.collide.OverlapPoint(other.transform.position))
                    return ladder;
            }
            return null;
        }

        public static List<Ladder> GetAll()
        {
            return ladder_list;
        }
    }

}