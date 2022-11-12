using System;
using TMPro;
using UnityEngine;

public class ClearTimer : MonoBehaviour
{
    [SerializeField] private VoidEventChannel playerReachExitEventChannel;
    [SerializeField] private StringEventChannel clearTimeEventChannel;

    private TMP_Text text;

    private bool stop;
    private float clearTime;
    private String ClearTimeText => TimeSpan.FromSeconds(clearTime).ToString(@"mm\:ss\:ff");

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        playerReachExitEventChannel.AddListener(LevelClear);
    }

    private void OnDisable()
    {
        playerReachExitEventChannel.RemoveListener(LevelClear);
    }

    private void Update()
    {
        if (stop) return;
        clearTime += Time.deltaTime;
        text.text = ClearTimeText;
    }

    private void LevelClear()
    {
        stop = true;
        clearTimeEventChannel.Broadcast(ClearTimeText);
    }
}