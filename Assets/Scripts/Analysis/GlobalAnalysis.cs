using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public static class GlobalAnalysis {
    // public const Int32 BUFFER_SIZE = 512; // Unmodifiable
    // public static String FILE_NAME = "Output.txt"; // Modifiable
    // public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
    public static int num_players;
    public static int num_bosses;
    public static int player_remaining_healthpoints;

    public static int boss_remaining_healthpoints;

    private static String URL = "https://cs526-fc451-default-rtdb.firebaseio.com/raw1/";




    public static string getTimeStamp()
    {
        return System.DateTime.Now.ToString();
    }

    public static IEnumerator postRequest(string key, string json)
    {
        var uwr = new UnityWebRequest(URL + key + ".json", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        string timestamp = getTimeStamp();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error While Sending: " + uwr.error + " TimeStamp: " + timestamp);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text + " TimeStamp: " + timestamp);
        }

        // if (uwr.isNetworkError)
        // {
        //     Debug.Log("Error While Sending: " + uwr.error);
        // }
        // else
        // {
        //     Debug.Log("Received: " + uwr.downloadHandler.text);
        // }
    }
}