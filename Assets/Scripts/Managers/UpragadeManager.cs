using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpragadeManager : MonoBehaviour
{
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimerBtnCallBack()
    {
        onTimerPurchased?.Invoke();

    }
    public void SizeBtnCallBack()
    {
        onSizePurchased?.Invoke();
    }
    public void PowerBtnCallBack()
    {
        onPowerPurchased?.Invoke();
    }
}
