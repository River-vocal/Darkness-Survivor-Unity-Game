using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controls for platformer demo
/// Author: Indie Marc (Marc-Antoine Desbiens)
/// </summary>

namespace IndieMarc.Platformer
{

    public class PlayerControls : MonoBehaviour
    {
        public int player_id;
        public KeyCode jump_key;
        public KeyCode action_key;
        public KeyCode menu_key;

        [HideInInspector]
        public bool disable_controls = false;

        private Vector2 move = Vector2.zero;
        private bool jump_press = false;
        private bool jump_hold = false;
        private bool action_press = false;
        private bool action_hold = false;
        private bool menu_press = false;

        private static Dictionary<int, PlayerControls> controls = new Dictionary<int, PlayerControls>();

        void Awake()
        {
            controls[player_id] = this;
        }

        void OnDestroy()
        {
            controls.Remove(player_id);
        }

        void Update()
        {
            move = Vector2.zero;
            jump_hold = false;
            jump_press = false;
            action_hold = false;
            action_press = false;
            menu_press = false;

            if (disable_controls)
                return;

            if (Input.GetKey(KeyCode.A))
                move += -Vector2.right;
            if (Input.GetKey(KeyCode.D))
                move += Vector2.right;
            if (Input.GetKey(KeyCode.W))
                move += Vector2.up;
            if (Input.GetKey(KeyCode.S))
                move += -Vector2.up;

            if (Input.GetKey(KeyCode.LeftArrow))
                move += -Vector2.right;
            if (Input.GetKey(KeyCode.RightArrow))
                move += Vector2.right;
            if (Input.GetKey(KeyCode.UpArrow))
                move += Vector2.up;
            if (Input.GetKey(KeyCode.DownArrow))
                move += -Vector2.up;

            if (Input.GetKey(jump_key))
                jump_hold = true;
            if (Input.GetKeyDown(jump_key))
                jump_press = true;
            if (Input.GetKey(action_key))
                action_hold = true;
            if (Input.GetKeyDown(action_key))
                action_press = true;
            if (Input.GetKeyDown(menu_key))
                menu_press = true;

            float move_length = Mathf.Min(move.magnitude, 1f);
            move = move.normalized * move_length;
        }


        //------ These functions should be called from the Update function, not FixedUpdate
        public Vector2 GetMove()
        {
            return move;
        }

        public bool GetJumpDown()
        {
            return jump_press;
        }

        public bool GetJumpHold()
        {
            return jump_hold;
        }

        public bool GetActionDown()
        {
            return action_press;
        }

        public bool GetActionHold()
        {
            return action_hold;
        }

        public bool GetMenuDown()
        {
            return menu_press;
        }

        //-----------

        public static PlayerControls Get(int player_id)
        {
            foreach (PlayerControls control in GetAll())
            {
                if (control.player_id == player_id)
                {
                    return control;
                }
            }
            return null;
        }

        public static PlayerControls[] GetAll()
        {
            PlayerControls[] list = new PlayerControls[controls.Count];
            controls.Values.CopyTo(list, 0);
            return list;
        }

    }

}