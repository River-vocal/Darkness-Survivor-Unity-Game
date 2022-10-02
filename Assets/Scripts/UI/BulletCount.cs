using TMPro;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    [SerializeField] GameObject textDisplay;
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected int bulletNum;
    // Start is called before the first frame update
    void Start()
    {
        bulletNum = playerController.getBulletCount();
        textDisplay.GetComponent<TMP_Text>().text = ": x" + bulletNum;
    }

    // Update is called once per frame
    void Update()
    {
        bulletNum = playerController.getBulletCount();
        textDisplay.GetComponent<TMP_Text>().text = ": x" + bulletNum;
    }
}
