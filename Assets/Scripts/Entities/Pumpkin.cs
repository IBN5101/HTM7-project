using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Pumpkin : Entity
{
    [SerializeField] private float forceMulti = 50f;

    private Rigidbody2D rigidBody2D;

    private Vector2 moveDirection;

    private void Awake() 
    { 
        GameManager.OnBeforeStateChanged += OnStateChanged;

        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        
    }

    private void Update()
    {
        // Update GridSystem
        UpdateGridPosition();

        // Check when the pumpkin is fully resting to allow movement again
        CheckEntityStable();

        // Only allow interaction when it's the entity turn
        if (GameManager.Instance.State != GameState.CHARACTERMOVE) return;

        // Keyboard input
        if (HandleInput())
            ExecuteMove();
    }   

    private void CheckEntityStable()
    {
        // Only allow interaction when game is running
        if (GameManager.Instance.State != GameState.RUNNING) return;

        if (rigidBody2D.velocity == Vector2.zero)
            GameManager.Instance.ChangeState(GameState.CHARACTERMOVE);
    }

    private void ExecuteMove()
    {
        // Override this to do some hero-specific logic, then call this base method to clean up the turn
        Debug.Log(moveDirection);
        rigidBody2D.AddForce(moveDirection * forceMulti);

        GameManager.Instance.ChangeState(GameState.RUNNING);
    }

    private bool HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Vector2.up;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Vector2.down;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Vector2.left;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Vector2.right;
            return true;
        }
        return false;
    }
}
