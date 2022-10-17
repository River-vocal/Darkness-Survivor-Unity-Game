using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AnalysisSender : Singleton<AnalysisSender>
{
    private static String URL = "https://cs526-fc451-default-rtdb.firebaseio.com/raw4/";

    public void postRequest(string key, string json) {
        Debug.Log("Store data into: " + key);
        StartCoroutine(_postRequest(key, json));
    }
    private IEnumerator _postRequest(string key, string json)
    {
        
        // var uwr = new UnityWebRequest(URL + key + ".json", "POST");
        // byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        // uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        // uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        // uwr.SetRequestHeader("Content-Type", "application/json");

        using (var uwr = new UnityWebRequest(URL + key + ".json", "POST")) {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            using UploadHandlerRaw uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.uploadHandler = uploadHandler;
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.disposeUploadHandlerOnDispose = true;
            uwr.disposeDownloadHandlerOnDispose = true;
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.timeout = 5;
            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            string timestamp = GlobalAnalysis.getTimeStamp();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error While Sending: " + uwr.error + " TimeStamp: " + timestamp);
            }
            else
            {
                Debug.Log("Data Received: " + uwr.downloadHandler.text + " TimeStamp: " + timestamp);
            }
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
