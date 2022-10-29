using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Pumpkin : Entity
{
    public event EventHandler OnGridPositionUpdated;

    [SerializeField] private float forceMulti = 50f;

    private Rigidbody2D rigidBody2D;

    private Vector2 moveDirection;

    private void Awake() 
    { 
        entityName = "pumpkin";

        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody2D.simulated = false;
        InitializeGridPosition();
        SnapToGrid();
        rigidBody2D.simulated = true;
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
            return true;
        }
        return false;
    }

    protected override void UpdateGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit changed GridPosition
            LevelGrid.Instance.EntityMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;

            OnGridPositionUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
