using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayInfo {
    public string level;
    public string state;
    public string timestamp;
    public int player_initail_healthpoints;
    public int boss_initail_healthpoints;
    public int player_remaining_healthpoints;
    public int boss_remaining_healthpoints;
    public int attack_number;
    public int critical_attack_number;
    public int bullet_attack_number;

    public PlayInfo(string level, string state, string timestamp, int p_ini_HP, int b_ini_HP, int p_rem_HP, int b_rem_HP, int a_num, int c_a_num, int b_a_m) {
        this.level = level;
        this.state = state;
        this.timestamp = timestamp;
        this.player_initail_healthpoints = p_ini_HP;
        this.boss_initail_healthpoints = b_ini_HP;
        this.player_remaining_healthpoints = p_rem_HP;
        this.boss_remaining_healthpoints = b_rem_HP;
        this.attack_number = a_num;
        this.critical_attack_number = c_a_num;
        this.bullet_attack_number = b_a_m;
    }
}