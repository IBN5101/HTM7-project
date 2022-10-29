using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GridPosition gridPosition;
    public string entityName { protected set; get; }

    private void Awake()
    {
        entityName = "Entity";
    }

    private void Start()
    {
        InitializeGridPosition();
        SnapToGrid();
    }

    protected void InitializeGridPosition()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddEntityAtGridPosition(gridPosition, this);
    }

    protected virtual void UpdateGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit changed GridPosition
            LevelGrid.Instance.EntityMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    protected void SnapToGrid()
    {
        transform.position = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

}
