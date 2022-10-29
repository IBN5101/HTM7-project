using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Entity> entities;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        this.entities = new List<Entity>();
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
    }

    public List<Entity> GetEntites()
    {
        return entities;
    }

    public override string ToString()
    {
        string entitiesString = "";
        foreach (Entity entity in entities)
            entitiesString += entity + "\n";
        return gridPosition.ToString() + "\n" + entitiesString;

    }
}
