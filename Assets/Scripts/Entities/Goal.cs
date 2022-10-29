using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Entity
{
    [SerializeField] private Entity linkedPumpkin;

    private void Awake()
    {
        entityName = "Goal";
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.WIN)
        {
            List<Entity> entities = LevelGrid.Instance.GetEntitiesAtGridPosition(gridPosition);
            if (entities.Contains(linkedPumpkin))
            {
                Debug.Log("YOU WIN!");
                GameManager.Instance.ChangeState(GameState.WIN);
            }
        }
    }
}
