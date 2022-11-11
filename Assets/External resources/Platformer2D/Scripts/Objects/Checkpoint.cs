using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IndieMarc.Platformer
{
    [RequireComponent(typeof(UniqueID))]
    public class Checkpoint : MonoBehaviour
    {
        public float range = 2f;
        public GameObject active_obj;
        public GameObject inactive_obj;

        string unique_id;
        bool active = false;
        float timer = 0f;

        private static List<Checkpoint> checkpoints = new List<Checkpoint>();

        void Awake()
        {
            checkpoints.Add(this);
            unique_id = GetComponent<UniqueID>().unique_id;
        }

        private void OnDestroy()
        {
            checkpoints.Remove(this);
        }

        void Start()
        {


        }

        void Update()
        {
            PlayerData pdata = PlayerData.Get();
            if (pdata == null)
                return;

            active = (pdata.current_checkpoint == unique_id);
            if (active_obj)
            {
                if (!active && active_obj.activeSelf)
                    active_obj.SetActive(false);
                if (active && !active_obj.activeSelf)
                    active_obj.SetActive(true);
            }
            if (inactive_obj)
            {
                if (active && inactive_obj.activeSelf)
                    inactive_obj.SetActive(false);
                if (!active && !inactive_obj.activeSelf)
                    inactive_obj.SetActive(true);
            }

            timer += Time.deltaTime;

            bool can_activate = timer > 15f || !active;
            foreach (PlayerCharacter character in PlayerCharacter.GetAll())
            {
                float dist = (transform.position - character.transform.position).magnitude;
                if (dist < range && can_activate)
                {
                    timer = 0f;
                    active = true;
                    character.SetRespawnPoint(transform.position);
                    Save();
                }
            }
        }

        public void Save()
        {
            PlayerData.Get().current_checkpoint = unique_id;
            TheGame.Get().Save();
        }

        public bool IsActive()
        {
            return active;
        }

        public string GetUID() {
            return unique_id;
        }

        public static Checkpoint Get(string uid)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.GetUID() == uid)
                    return checkpoint;
            }
            return null;
        }
    }
}
