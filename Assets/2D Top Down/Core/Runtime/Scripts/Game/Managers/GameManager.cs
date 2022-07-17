using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Menu,
    Start,
    GameEnd
}

public class GameManager : MonoBehaviour
{
    #region Variables
    
    public static GameManager Instance;

    private GameState _state;

    [SerializeField]
    private UnityEvent<GameState> OnChangeState;
    
    [SerializeField] 
    private UnityEvent OnGameWin;

    #endregion

    #region Mono Behaviours

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        StartGame();
    }

    #endregion

    #region Methods
    public GameState State
    {
        get { return _state; }
        set
        {
            _state = value;
            OnChangeState?.Invoke(_state);
        }
    }
    
    public void StartGame()
    {
        State = GameState.Start;
    }

    public void GameWin()
    {
        State = GameState.GameEnd;

        OnGameWin?.Invoke();
    }

    #endregion
}
