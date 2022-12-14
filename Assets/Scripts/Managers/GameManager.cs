using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    [SerializeField] private string _sceneName;
    [SerializeField] private string _nextSceneName;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

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
                HandleLosing();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandleStarting() { 
        ChangeState(GameState.RUNNING);
    }

    private void HandleWinning() {
        _winScreen.SetActive(true);
        _loseScreen.SetActive(false);
    }

    private void HandleLosing() {
        _winScreen.SetActive(false);
        _loseScreen.SetActive(true);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(_nextSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadScene();
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