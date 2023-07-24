using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum GameState {MENU,GAME,LEVELCOMPLETE,GAMEOVER }

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    private GameState state;

    [Header("Events")]
    public static Action<GameState> onStateChanged;


    IEnumerator Start()
    {
        yield return null;

        state = GameState.MENU;
        onStateChanged?.Invoke(state);
    }


    void Update()
    {
        
    }
    public void SetGameState()
    {
        state = GameState.GAME;
        onStateChanged?.Invoke(state);
    }
}
