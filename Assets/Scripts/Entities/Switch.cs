using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Switch : Entity
{
    [SerializeField] private Pumpkin linkedPumpkin;
    [SerializeField] private Wall linkedWall;

    private void Awake()
    {
        entityName = "Switch";
    }

    private void Start()
    {
        linkedPumpkin.OnGridPositionUpdated += Pumpkin_OnGridPositionUpdated;

        InitializeGridPosition();
        SnapToGrid();
    }

    private void Pumpkin_OnGridPositionUpdated(object sender, EventArgs empty)
    {
        List<Entity> entities = LevelGrid.Instance.GetEntitiesAtGridPosition(gridPosition);
        if (entities.Contains(linkedPumpkin))
        {
            LevelGrid.Instance.RemoveEntityAtGridPosition(gridPosition, this);
            LevelGrid.Instance.RemoveEntityAtGridPosition(linkedWall.gridPosition, linkedWall);

            Destroy(linkedWall.gameObject);
            Destroy(this.gameObject);
        }
    }
}