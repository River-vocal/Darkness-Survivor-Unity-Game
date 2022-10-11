using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

/*
 * This class will store and "broadcast" desired audio info
 * of the audio source attached to this game object.
 */
public class AudioInfoBroadcaster : MonoBehaviour
{
    public AudioInfoBroadcasterData broadcasterData;

    [Header("Broadcast Properties")] 
    
    public float updateStep = .01f;
    public int sampleDataLength = 128;

    private IEnumerator coroutine;
    private AudioSource audioSource;
    private float curUpdateTime;
    private float[] clipSampleData;
    // Var to broadcast
    public float clipLoudness;
    
    /*
     * Adding each broadcast var to an enum, so other scripts
     * can know which value to grab based on this
     */
    
    public enum AudioBroadcastValueType
    {
        clipLoudness = 1,
        hasVolume = 2,
    }

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coroutine = UpdateVolume();
        StartCoroutine(coroutine);
    }

    IEnumerator UpdateVolume()
    {
        while (true)
        {
            while (audioSource == null || !audioSource.isPlaying)
            {
                yield return null;
            }
    
            // curUpdateTime += Time.deltaTime;
            while (audioSource.clip.loadState != AudioDataLoadState.Loaded)
            {
                yield return null;
            }
            yield return new WaitForSeconds(updateStep);
            // if (curUpdateTime >= updateStep)
            // {
            if (audioSource.clip.samples - audioSource.timeSamples < 1000)
            {
                continue;
            }

            // Debug.Log("Before getting data " + audioSource.timeSamples);
            if (audioSource.clip.samples - audioSource.timeSamples < 1000)
            {
                Debug.Log(audioSource.timeSamples + " Something is wronggggggggggggggggggggggggggggggggggggggg!!!!!!!");
            }
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            // Debug.Log("After getting data ");
            clipLoudness = 0;
            
            // Get the average loudness for the given sample.
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleDataLength;
            // StartCoroutine(UpdateVolume());
            // }
        }
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (audioSource == null || !audioSource.isPlaying)
        {
            return;
        }

        curUpdateTime += Time.deltaTime;
        if (audioSource.clip.loadState != AudioDataLoadState.Loaded)
        {
            return;
        }
        if (curUpdateTime >= updateStep)
        {
            curUpdateTime = 0;
            Debug.Log("Before getting data " + audioSource.timeSamples);
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            Debug.Log("After getting data " + clipSampleData);
            clipLoudness = 0;
            
            // Get the average loudness for the given sample.
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleDataLength;
        }
    }*/
    
    
    /// <summary>
    /// Ask this class for the effector value of the given type.
    /// </summary>
    /// <param name="audioBroadcastValueType"></param>
    /// <returns></returns>
    public float GetEffectorValue(AudioBroadcastValueType audioBroadcastValueType)
    {
        float effectorValue = 0;
        switch (audioBroadcastValueType)
        {
            case AudioBroadcastValueType.clipLoudness:
                effectorValue = clipLoudness;
                break;
            
            case AudioBroadcastValueType.hasVolume:
                /*
                 * a's value of 0/1 whether cur track has any volume or not
                 * 1 == any kind of sound
                 * 0 == silence
                 */
                if (clipLoudness > 0)
                {
                    effectorValue = 1;
                }
                else
                {
                    effectorValue = 0;
                }

                break;
            
            default:
                Debug.LogError("Unknown AudioBroadcastValue");
                break;
        }

        return effectorValue;
    }

    /// <summary>
    /// Set the broadcasterData when starting a new project.
    /// Some of the broadcasterData we can set using the attached audio clip, some we will need to know when the
    /// user selects the track to add to the project - the latter will be passed in the params to this method.
    /// </summary>
    /// <param name="trackFullPath"></param>
    /// <param name="trackLocalPath"></param>
    /// <param name="parentGameObjectName"></param>
    /// <param name="fileSize"></param>
    public void SetData(string trackFullPath, string trackLocalPath, string parentGameObjectName, float fileSize)
    {
        broadcasterData.trackName = audioSource.clip.name;
        broadcasterData.trackFullPath = trackFullPath;
        broadcasterData.trackLocalPath = trackLocalPath;
        broadcasterData.parentGameObjectName = parentGameObjectName;
        broadcasterData.trackLength = audioSource.clip.length;
        broadcasterData.fileSize = fileSize;
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.Play();
    }
    
    // No "AudioClipLoader" class instruction in the original tutorial
    // https://www.youtube.com/watch?v=BTAD1-zSYO4
    /*public async void SetAudioClipFromData()
    {
        AudioClipLoader clipLoader = new AudioClipLoader();
        try
        {
            audioSource.clip = await clipLoader.LoadClip(broadcasterData.trackFullPath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading audio clip: " + ex.Message);
        }
    }*/
}
