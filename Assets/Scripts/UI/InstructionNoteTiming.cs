using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionNoteTiming : AudioEffector
{
    [SerializeField] GameObject timingNote;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        timingNote.SetActive(false);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (effectorValue > 0.2f)
        {
            timingNote.SetActive(true);
        }
        else
        {
            timingNote.SetActive(false);
        }
        
    }
}
