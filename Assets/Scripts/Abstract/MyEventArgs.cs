using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IntegerEventArg : EventArgs
{
    private int value;
    public int Value{
        get {
            return value;
        }
    }
    public IntegerEventArg(int value){
        this.value = value;
    }
}
