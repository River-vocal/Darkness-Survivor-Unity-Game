using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public static class GlobalAnalysis {
    public static string level;
    public static string scene;
    public static string state;
    public static string timestamp;
    public static int player_initail_healthpoints;
    public static int boss_initail_healthpoints;
    public static int player_remaining_healthpoints;
    public static int boss_remaining_healthpoints;
    public static int attack_number;
    public static int critical_attack_number;
    public static int bullet_attack_number;
    public static long start_time;

    public static string buildPlayInfoData() {
        PlayInfo pi = new PlayInfo(level, 
        scene,
        state, 
        getTimeStamp(), 
        (new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - start_time).ToString(),
        player_initail_healthpoints, 
        boss_initail_healthpoints, 
        player_remaining_healthpoints, 
        boss_remaining_healthpoints,
        attack_number,
        critical_attack_number,
        bullet_attack_number);
        string json = JsonUtility.ToJson(pi);
        return json;
    }
    public static void cleanData() {
        level = "N/A";
        state = "N/A";
        timestamp = "N/A";
        scene = "N/A";
        player_initail_healthpoints = -1;
        boss_initail_healthpoints = -1;
        player_remaining_healthpoints = -1;
        player_remaining_healthpoints = -1;
        attack_number = 0;
        critical_attack_number = 0;
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