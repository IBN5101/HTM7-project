using System;
using UnityEngine;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

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
                HandleSpawningHeroes();
                break;
            case GameState.WIN:
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

    private void HandleSpawningHeroes() {
        CharacterManager.Instance.SpawnHeroes();
        
        //ChangeState(GameState.SpawningEnemies);
    }

    private void HandleSpawningEnemies() {
        
        // Spawn enemies
        
        //ChangeState(GameState.HeroTurn);
    }

    private void HandleHeroTurn() {
        // If you're making a turn based game, this could show the turn menu, highlight available units etc
        
        // Keep track of how many units need to make a move, once they've all finished, change the state. This could
        // be monitored in the unit manager or the units themselves.
    }
}

[Serializable]
public enum GameState {
    SETUP = 0,
    RUNNING = 1,
    CHARACTERMOVE = 2,
    WIN = 2,
    LOSE = 3,
}