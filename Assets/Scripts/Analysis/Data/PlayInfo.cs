using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class PlayInfo {
    public string level;
    public string state;
    public string player_status;
    public double healing_energy;
    public double trap_damage;
    public double boss_damage;
    public double light_damage;
    public bool is_boss_killed;
    public string timestamp;
    public string time_diff;

    public PlayInfo(string level, string state, string player_status, double heal, double trap_dmg, double boss_dmg, double light_dmg, bool boss_killed, string timestamp, string time_diff) {
        this.level = level;
        this.state = state;
        this.state = state;
        this.player_status = player_status;
        this.healing_energy = heal;
        this.trap_damage = trap_dmg;
        this.boss_damage = boss_dmg;
        this.light_damage = light_dmg;
        this.is_boss_killed = boss_killed;
        this.timestamp = timestamp;
        this.time_diff = time_diff;
    }
}