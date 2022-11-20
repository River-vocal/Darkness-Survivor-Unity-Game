using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTextController : MonoBehaviour
{
    [SerializeField] private GameObject textDisplay;
    [SerializeField] protected Player player;
    private int bulletNum;

    // Start is called before the first frame update
    void Start()
    {
        bulletNum = player.playerBulletCount;
        textDisplay.GetComponent<TMP_Text>().text = ": x" + bulletNum;
    }

    // Update is called once per frame
    void Update()
    {
        bulletNum = player.playerBulletCount;
        textDisplay.GetComponent<TMP_Text>().text = ": x" + bulletNum;
    }
}
