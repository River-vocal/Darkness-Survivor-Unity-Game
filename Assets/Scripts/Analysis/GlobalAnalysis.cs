using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public static class GlobalAnalysis {
    public static string level;
    public static string state;
    public static string player_status;
    public static double healing_energy;
    public static double trap_damage;
    public static double boss_damage;
    public static double light_damage;
    public static bool is_boss_killed;
    public static string timestamp;
    public static long start_time;
    public static string time_diff;

    public static string buildPlayInfoData() {
        PlayInfo pi = new PlayInfo(
            level,
            state,
            player_status,
            healing_energy,
            trap_damage,
            boss_damage,
            light_damage,
            is_boss_killed,
            getTimeStamp(), 
            (new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - start_time).ToString());
        string json = JsonUtility.ToJson(pi);
        return json;
    }
    public static void cleanData() {
        level = "N/A";
        state = "N/A";
        player_status = "N/A";
        timestamp = "N/A";
        healing_energy = 0;
        trap_damage = 0;
        boss_damage = 0;
        light_damage = 0;
        is_boss_killed = false;
    }

    public static void init() {
        start_time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); 
        player_status = "default";
    }
    public static string getTimeStamp()
    {
        return System.DateTime.Now.ToString();
    }

    // public static IEnumerator postRequest(string key, string json)
    // {
    //     var uwr = new UnityWebRequest(URL + key + ".json", "POST");
    //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    //     uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
    //     uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //     uwr.SetRequestHeader("Content-Type", "application/json");

    //     //Send the request then wait here until it returns
    //     yield return uwr.SendWebRequest();

    //     string timestamp = getTimeStamp();

    //     if (uwr.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.Log("Error While Sending: " + uwr.error + " TimeStamp: " + timestamp);
    //     }
    //     else
    //     {
    //         Debug.Log("Received: " + uwr.downloadHandler.text + " TimeStamp: " + timestamp);
    //     }

    //     // if (uwr.isNetworkError)
    //     // {
    //     //     Debug.Log("Error While Sending: " + uwr.error);
    //     // }
    //     // else
    //     // {
    //     //     Debug.Log("Received: " + uwr.downloadHandler.text);
    //     // }
    // }
}