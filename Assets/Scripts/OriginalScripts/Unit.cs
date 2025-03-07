using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int movementRange = 3;
    public int attackRange = 3;
    public bool isEnemy = true;

    public int movesPerTurn = 1;
    public int movesAvailable;
    public int attacksPerTurn = 1;
    public int attacksAvailable;

    public Tile currentTile;

    private void Start()
    {
        UnitManager.Instance.AddUnit(this);
        if (!currentTile)
            GridManager.Instance.Get(transform.position).PlaceUnit(this);

        movesAvailable = movesPerTurn;
        attacksAvailable = attacksPerTurn;

        TurnManager.Instance.OnTurnEnd += TurnEnd;
    }
    private void OnDestroy()
    {
        UnitManager.Instance?.RemoveUnit(this);
    }


    public void Move(Tile destination)
    {
        if (movesAvailable <= 0)
            return;

        movesAvailable--;
        UnitMovement.MoveUnit(this, destination);
    }

    public void Attack(Unit target)
    {
        if (!target) return;
        if (attacksAvailable <= 0) return;

        attacksAvailable--;

        if (target && (target.isEnemy != isEnemy))
            Destroy(target.gameObject);
    }

    public void TurnEnd()
    {
        movesAvailable = movesPerTurn;
        attacksAvailable = attacksPerTurn;
    }

    public bool CanDoAction() => movesAvailable > 0 || attacksAvailable > 0;

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
