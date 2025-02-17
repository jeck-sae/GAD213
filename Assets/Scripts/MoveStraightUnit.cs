using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveStraightUnit : Unit
{
    public override List<Tile> GetWalkableTiles(Tile center)
    {
        var walkable = new List<Tile>();

        Func<Tile, bool> stopLookingCondition = (Tile t) => { return t && t.IsWalkable; };

        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.down, Vector2Int.down, movementRange, stopLookingCondition));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.left, Vector2Int.left, movementRange, stopLookingCondition));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.up, Vector2Int.up, movementRange, stopLookingCondition));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position, Vector2Int.right, movementRange, stopLookingCondition));

        return walkable.Where(x => x.unit == null).ToList();
    }
}
