using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Transform gridDebugObjectPrefab;
    [Header("Params")]
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 2;
    [SerializeField] private bool testing = true;

    private GridSystem gridSystem;

    private void Awake()
    {
        Instance = this;

        gridSystem = new GridSystem(gridWidth, gridHeight, cellSize);
        if (testing)
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddEntityAtGridPosition(GridPosition gridPosition, Entity entity)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddEntity(entity);
    }

    public void RemoveEntityAtGridPosition(GridPosition gridPosition, Entity entity)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveEntity(entity);
    }

    public List<Entity> GetEntitiesAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetEntites();
    }

    public void EntityMovedGridPosition(Entity entity, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveEntityAtGridPosition(fromGridPosition, entity);
        AddEntityAtGridPosition(toGridPosition, entity);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
}
