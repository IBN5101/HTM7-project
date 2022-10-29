using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadZone : Entity
{
    [SerializeField] private Pumpkin linkedPumpkin;

    private void Awake()
    {
        entityName = "Bad zone";
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
            ParticleGenrator.Instance.GenerateLoseParticle(transform.position);
            GameManager.Instance.ChangeState(GameState.LOSE);
        }
    }
}
