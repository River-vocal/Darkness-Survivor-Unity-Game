using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffector : MonoBehaviour
{
    // broadcaster
    [SerializeField] protected AudioInfoBroadcaster audioInfoBroadcaster;

    [SerializeField] protected AudioInfoBroadcaster.AudioBroadcastValueType effectorType;

    [SerializeField] protected float effectorValue;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (audioInfoBroadcaster == null)
        {
            Debug.LogError("Unassigned audioInfoBroadcaster.");
        }
        
    }

    // Update is called once per frame
    public virtual void  Update()
    {
        effectorValue = audioInfoBroadcaster.GetEffectorValue(effectorType);
    }
}
