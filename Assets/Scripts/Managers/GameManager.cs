using System;
using UnityEngine;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    [SerializeField] private GameObject _winScreen;

    public GameState State { get; private set; }

    void Start() => ChangeState(GameState.SETUP);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.SETUP:
                HandleStarting();
                break;
            case GameState.RUNNING:
            case GameState.CHARACTERMOVE:
                break;
            case GameState.WIN:
                HandleWinning();
                break;
            case GameState.LOSE:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting() { 
        ChangeState(GameState.RUNNING);
    }

    private void HandleWinning() {
        _winScreen.SetActive(true);
    }
}

[Serializable]
public enum GameState {
    SETUP = 0,
    RUNNING = 1,
    WIN = 2,
    LOSE = 3,
    CHARACTERMOVE = 4,
}