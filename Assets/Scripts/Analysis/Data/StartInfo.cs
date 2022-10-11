using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class StartInfo {
    public string level;
    public string timestamp;
    
    public StartInfo(string level, string timestamp) {
      this.timestamp = timestamp;
      this.level = level;
    }
}