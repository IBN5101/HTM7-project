using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadZone : Entity
{
    [SerializeField] private Entity linkedPumpkin;

    private void Awake()
    {
        entityName = "Bad zone";
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.LOSE)
        {
            List<Entity> entities = LevelGrid.Instance.GetEntitiesAtGridPosition(gridPosition);
            if (entities.Contains(linkedPumpkin))
            {
                Debug.Log("YOU LOSE!");
                GameManager.Instance.ChangeState(GameState.LOSE);
            }
        }
    }
}
