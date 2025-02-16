using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveStraightUnit : Unit
{
    public override List<Tile> GetWalkableTiles(Tile center)
    {
        var walkable = new List<Tile>();
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position, Vector2Int.right, movementRange, (Tile t) => { return t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.down, Vector2Int.down, movementRange, (Tile t) => { return t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.left, Vector2Int.left, movementRange, (Tile t) => { return t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.up, Vector2Int.up, movementRange, (Tile t) => { return t.IsWalkable; }));
        return walkable.Where(x => x.unit == null).ToList();
    }
}
