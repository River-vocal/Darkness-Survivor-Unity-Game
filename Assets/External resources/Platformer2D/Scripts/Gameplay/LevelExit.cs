using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{
    public class LevelExit : MonoBehaviour
    {
        [Header("Entrance")]
        public int index;
        public Vector2 entrance_offset;

        [Header("Exit")]
        public string go_to_level = "";
        public int go_to_index = 0;

        [Header("Audio")]
        public float end_delay = 0.5f;
        public AudioClip audio_end;

        private static List<LevelExit> levelExits = new List<LevelExit>();

        private void Awake()
        {
            levelExits.Add(this);
        }

        private void OnDestroy()
        {
            levelExits.Remove(this);
        }

        void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }

        void Update()
        {

        }

        public Vector3 GetEntryPosition()
        {
            return transform.position + new Vector3(entrance_offset.x, entrance_offset.y, 0f);
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.GetComponent<PlayerCharacter>())
            {
                if (go_to_level != "")
                {
                    EndLevel();
                }
            }
        }

        public void EndLevel()
        {
            TheGame.Get().Pause();
            TheAudio.Instance.StopSound("music");
            TheAudio.Instance.PlaySound("level", audio_end);
            StartCoroutine(EndLevelRoutine(go_to_level, go_to_index));
        }

        private IEnumerator EndLevelRoutine(string level, int index)
        {
            BlackPanel.Get().Show();
            yield return new WaitForSeconds(end_delay);
            SceneNav.GoToLevel(level, index);
        }

        public static LevelExit Get(int index)
        {
            foreach (LevelExit exit in levelExits)
            {
                if (exit.index == index)
                {
                    return exit;
                }
            }
            return null;
        }
    }

}