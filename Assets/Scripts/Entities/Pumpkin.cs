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

    private void Start()
    {
        rigidBody2D.simulated = false;
        InitializeGridPosition();
        SnapToGrid();
        rigidBody2D.simulated = true;
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        
    }

    private void Update()
    {
        // Update GridSystem
        UpdateGridPosition();

        switch (GameManager.Instance.State)
        {
            case GameState.RUNNING:
                CheckEntityStable();
                break;
            case GameState.CHARACTERMOVE:
                HandleMove();
                break;
            default:
                break;
        }
    }   

    private void CheckEntityStable()
    {
        if (rigidBody2D.velocity.magnitude < 0.001f)
        {
            GameManager.Instance.ChangeState(GameState.CHARACTERMOVE);
        }
    }

    private void HandleMove()
    {
        if (HandleInput())
        {
            Debug.Log(moveDirection);
            rigidBody2D.AddForce(moveDirection * forceMulti);
            moveDirection = Vector2.zero;

            GameManager.Instance.ChangeState(GameState.RUNNING);
        }
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
