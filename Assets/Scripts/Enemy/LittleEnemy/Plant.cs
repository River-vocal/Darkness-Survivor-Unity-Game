using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
    [SerializeField] public float attackTime;
    private float currTime;
    private int damage;
    [SerializeField] public Transform pos1;
    [SerializeField] public GameObject tentacle;

    private void Awake()
	{
		damage = littleEnemy.damage;
	}

    // Start is called before the first frame update
    void Start()
    {
        currTime = attackTime;
        float diffY = gameObject.transform.position.y - pos1.position.y;
        float scaleMulti = diffY / 4.5f - 1;
        Vector3 scaleChange = new Vector3(0f, scaleMulti, 0);
        tentacle.transform.localScale += scaleChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime <= 0) {
            currTime = attackTime;
        }
        else if (currTime <= 0.3) {
            GetComponent<Animator>().SetTrigger("Attack2");
            tentacle.gameObject.SetActive(true);
            currTime -= Time.deltaTime;
        }
        else {
            tentacle.gameObject.SetActive(false);
            currTime -= Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, pos1.position);
    }

}
