using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSize : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image fillImage;

    [Header(" Settings ")]
    [SerializeField] private float scaleIncreaseThreshold;
    [SerializeField] private float scaleStep;
    private float scaleValue;

    [Header("Events")]
    public static Action<float> onIncrease;

    [Header("Power")]
    private float powerMultiplier;

    private void Start()
    {
        fillImage.fillAmount = 0;

        UpragadeManager.onSizePurchased += UpragadeManager_OnSizePurchased;
        UpragadeManager.onPowerPurchased += UpragadeManager_OnPowerPurchased;
    }
    private void OnDestroy()
    {
        UpragadeManager.onSizePurchased -= UpragadeManager_OnSizePurchased;
        UpragadeManager.onPowerPurchased -= UpragadeManager_OnPowerPurchased;
    }

    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;

        LeanTween.scale(transform.gameObject, targetScale * Vector3.one, .5f * Time.deltaTime * 60f)
            .setEase(LeanTweenType.easeInOutQuart);

        onIncrease?.Invoke(targetScale);
    }

    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize * (1 + powerMultiplier);

        if (scaleValue >= scaleIncreaseThreshold)
        {
            IncreaseScale();
            scaleValue = scaleValue % scaleIncreaseThreshold;
        }
        UpdateFillImage();
    }
    private void UpdateFillImage()
    {
        float targetFillAmount = scaleValue / scaleIncreaseThreshold;

        LeanTween.value(fillImage.fillAmount, targetFillAmount, .2f * Time.deltaTime * 60)
            .setOnUpdate(UpdateFillImageSmoothly);
    }
    private void UpdateFillImageSmoothly(float value)
    {
        fillImage.fillAmount = value;
    }
    private void UpragadeManager_OnSizePurchased()
    {
        IncreaseScale();
    }
    private void UpragadeManager_OnPowerPurchased()
    {
        powerMultiplier++;
    }
}
