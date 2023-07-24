using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerTimer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int timerDuration;
    int timer;
    private bool timerIsOn;

    public static Action onTimerOver;

    void Start()
    {
        Initialize();
        GameManager.onStateChanged += GameManager_OnStateChanged;
        UpragadeManager.onTimerPurchased += UpragadeManager_OnTimerPurchased;
    }
    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameManager_OnStateChanged;
        UpragadeManager.onTimerPurchased -= UpragadeManager_OnTimerPurchased;
    }
    void Update()
    {
        
    }
    public void Initialize()
    {
        timer = timerDuration;
        timerText.text = FormatSeconds(timer);
    }
    public void StartTimer()
    {
        if (timerIsOn)
        {
            return;
        }
        Initialize();
        timerIsOn = true;
        StartCoroutine(TimerCoroutine());
        
    }
    private void GameManager_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GAME:
                StartTimer();
                break;
        }
    }
    IEnumerator TimerCoroutine()
    {
        while (timerIsOn)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = FormatSeconds(timer);

            if (timer==0)
            {
                StopTimer();
            }
        }
    }
    public void StopTimer()
    {
        print("Timer is over");
        timerIsOn = false;
        onTimerOver?.Invoke();

    }
    private string FormatSeconds(int totalSecods)
    {
        int minutes = totalSecods / 60;
        int seconds = totalSecods % 60;

        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
    private void UpragadeManager_OnTimerPurchased()
    {
        timerDuration += 3;
        Initialize();
    }
}
