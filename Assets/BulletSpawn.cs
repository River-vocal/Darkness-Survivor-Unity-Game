using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    public GameObject ResourceBulletPrefab;
    [SerializeField] public float SecondSpawn = 5f;
    [SerializeField] public float MinTras;
    [SerializeField] public float MaxTras;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletGenerator());
    }

    IEnumerator BulletGenerator()
    {
        while (true)
        {
            var wanted = Random.Range(MinTras, MaxTras);
            var position = new Vector3(wanted, 3.96f);
            GameObject gameObject = Instantiate(ResourceBulletPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(SecondSpawn);
            Destroy(gameObject, 5f);
        }
    }

}
