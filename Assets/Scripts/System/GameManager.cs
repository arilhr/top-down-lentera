using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Start
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Menu;

    public Action<GameState> OnChangeState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartGame()
    {
        ChangeState(GameState.Start);
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        OnChangeState?.Invoke(newState);
    }
}
