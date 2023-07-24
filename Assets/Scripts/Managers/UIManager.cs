using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;


    void Start()
    {
        GameManager.onStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameManager_OnStateChanged;
    }

    void Update()
    {
        
    }
    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    private void GameManager_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GAME:
                SetGame();
                break;
            case GameState.MENU:
                SetMenu();
                break;
        }
    }
}
