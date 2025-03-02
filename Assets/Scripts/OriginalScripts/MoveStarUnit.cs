using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveStarUnit : Unit
{
    public int diagonalMovementRange = 2;
    public override List<Tile> GetWalkableTiles(Tile center)
    {
        var walkable = new List<Tile>();
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.up + Vector2Int.left, Vector2Int.up + Vector2Int.left, diagonalMovementRange, (Tile t) => {
            return t && t.IsWalkable && (GridManager.Instance.Get(t.position + Vector2Int.down).IsWalkable || GridManager.Instance.Get(t.position + Vector2Int.right).IsWalkable); }));
        
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.up + Vector2Int.right, Vector2Int.up + Vector2Int.right, diagonalMovementRange, (Tile t) => { 
            return t && t.IsWalkable && (GridManager.Instance.Get(t.position + Vector2Int.down).IsWalkable || GridManager.Instance.Get(t.position + Vector2Int.left).IsWalkable); }));
        
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.down + Vector2Int.left, Vector2Int.down + Vector2Int.left, diagonalMovementRange, (Tile t) => {
            return t && t.IsWalkable && (GridManager.Instance.Get(t.position + Vector2Int.up).IsWalkable || GridManager.Instance.Get(t.position + Vector2Int.right).IsWalkable); }));
        
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.down + Vector2Int.right, Vector2Int.down + Vector2Int.right, diagonalMovementRange, (Tile t) => { 
            return t && t.IsWalkable && (GridManager.Instance.Get(t.position + Vector2Int.up).IsWalkable || GridManager.Instance.Get(t.position + Vector2Int.left).IsWalkable); }));
        

        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.right, Vector2Int.right, movementRange, (Tile t) => { return t && t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.down, Vector2Int.down, movementRange, (Tile t) => { return t && t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.left, Vector2Int.left, movementRange, (Tile t) => { return t && t.IsWalkable; }));
        walkable.AddRange(GridManager.Instance.GetTilesInLine(center.position + Vector2Int.up, Vector2Int.up, movementRange, (Tile t) => { return t && t.IsWalkable; }));
        return walkable.Where(x => x.unit == null).ToList();
    }
}
