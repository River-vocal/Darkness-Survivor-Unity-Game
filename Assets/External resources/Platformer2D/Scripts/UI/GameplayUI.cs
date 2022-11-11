using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IndieMarc.Platformer
{

    public class GameplayUI : MonoBehaviour
    {
        public int player_id;
        public IconBar health;
        public Text coins;

        void Start()
        {

        }

        void Update()
        {
            PlayerData pdata = PlayerData.Get();
            if (pdata != null)
            {
                coins.text = pdata.coins.ToString();
            }

            PlayerCharacter character = PlayerCharacter.Get(player_id);
            if (character)
            {
                health.value = Mathf.FloorToInt( character.GetHP());
                health.value_max = Mathf.FloorToInt(character.max_hp);
            }
        }
    }

}