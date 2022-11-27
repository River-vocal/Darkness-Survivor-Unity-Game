using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psAttackManager : MonoBehaviour
{
    public HashSet<Collider2D> set;
    
    // Start is called before the first frame update
    void Start()
    {
        set = new HashSet<Collider2D>();
    }
}
