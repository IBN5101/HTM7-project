using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Entity
{
    [SerializeField] private Pumpkin linkedPumpkin;

    private void Awake()
    {
        entityName = "Goal";
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
            ParticleGenrator.Instance.GenerateWinParticle(transform.position);
            GameManager.Instance.ChangeState(GameState.WIN);
        }
    }
}
