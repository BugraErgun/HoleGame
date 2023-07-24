using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    private Vector3 clickedScreenPosition;
    [SerializeField] private float screenPositionFollowThreshold;
    [SerializeField] private float moveSpeed = 5f;

    public bool canMove;

    void Start()
    {
       

        PlayerTimer.onTimerOver += DisableMovement;
        GameManager.onStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        PlayerTimer.onTimerOver -= DisableMovement;
    }

    void Update()
    {
        ManageControl();
    }

    

    private void ManageControl()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickedScreenPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 difference = Input.mousePosition - clickedScreenPosition;

                Vector3 direction = difference.normalized;

                float maxScreenDistance = screenPositionFollowThreshold * Screen.height;

                if (difference.magnitude > maxScreenDistance)
                {
                    clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                    difference = Input.mousePosition - clickedScreenPosition;
                }

                difference /= Screen.width;

                difference.z = difference.y;
                difference.y = 0;

                Vector3 targetPosition = transform.position + difference * moveSpeed * Time.deltaTime;

                transform.position = targetPosition;
            }
        }
    }
    private void GameStateChangedCallBack(GameState state)
    {
        switch (state)
        {
            case GameState.GAME:
                EnableMovement();
                break;
        }
    }
    private void EnableMovement()
    {
        canMove = true;
    }
    private void DisableMovement()
    {
        canMove = false;
    }
}
