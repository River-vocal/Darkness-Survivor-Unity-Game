using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostMenu : MonoBehaviour
{
    [SerializeField] private Energy energy;
    [SerializeField] private GameObject lostMenu;

    private void Awake()
    {
        energy.OnEmpty += energy_OnEmpty;
    }

    private void energy_OnEmpty(object sender, System.EventArgs e)
    {
        lostMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
