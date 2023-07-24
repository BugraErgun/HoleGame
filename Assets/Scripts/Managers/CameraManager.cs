using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    [Header("Settings")]
    [SerializeField] private float minDistance;
    [SerializeField] private float distanceMultiplier;

    void Start()
    {
        PlayerSize.onIncrease += PlayerSize_onIncrease;
    }

    private void OnDestroy()
    {
        PlayerSize.onIncrease -= PlayerSize_onIncrease;
    }
    private void PlayerSize_onIncrease(float playerSize)
    {
        float distance = minDistance + (playerSize - 1) * distanceMultiplier;
        Vector3 targetCameraOffset = new Vector3(0, distance * 1.5f, -distance);

        LeanTween.value(gameObject, GetFollowOffset(), targetCameraOffset, .5f * Time.deltaTime * 60f)
            .setOnUpdate((Vector3 offset) => playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = offset);

    }
    private Vector3 GetFollowOffset()
    {
        return playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }
}
