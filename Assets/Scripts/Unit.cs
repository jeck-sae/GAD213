using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int movementRange = 3;
    public int attackRange = 3;
    public bool isEnemy = true;

    public Tile currentTile;

    private void Start()
    {
        UnitManager.Instance.AddUnit(this);
        if (!currentTile)
            GridManager.Instance.Get(transform.position).PlaceUnit(this);
    }

    private void OnDestroy()
    {
        UnitManager.Instance?.RemoveUnit(this);
    }

    public List<Tile> GetWalkableTiles() => GetWalkableTiles(currentTile);
    public virtual List<Tile> GetWalkableTiles(Tile center)
    {
        var tiles = Pathfinder.GetReachableTiles(GridManager.Instance, center, movementRange);
        tiles.Remove(center);
        return tiles.Where(x => x.unit == null).ToList();
    }
    public List<Tile> GetAttackableTiles() => GetAttackableTiles(currentTile);
    public virtual List<Tile> GetAttackableTiles(Tile center)
    {
        var tiles = GridManager.Instance.GetTilesInCross(center.position, attackRange);
        tiles.Remove(center);
        return tiles;
    }


}
