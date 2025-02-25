using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    public Tile SelectedTile => SelectedUnit?.currentTile;
    public Unit SelectedUnit;

    public List<Unit> Units = new List<Unit>();

    public void AddUnit(Unit unit)
    {
        if(!Units.Contains(unit))
            Units.Add(unit);
    }
    public void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            SelectedUnit = null;

        if (Input.GetMouseButtonDown(0))
        {
            if(UnitMovement.Instance.isMoving)
                return;

            var tile = GridManager.Instance.Get(Helpers.Camera.ScreenToWorldPoint(Input.mousePosition));

            //Move to tile
            if (SelectedUnit && SelectedUnit.GetWalkableTiles().Contains(tile))
            {
                SelectedUnit.Move(tile);
                return;
            }

            //Deselect unit
            /*if (SelectedUnit == tile?.unit)
            {
                SelectedUnit = null;
                return;
            }*/

            //Select unit
            if (tile && tile.unit && !tile.unit.isEnemy)
            {
                SelectedUnit = tile.unit;
                return;
            }

            //Attack
            if(tile && tile.unit && SelectedUnit && SelectedUnit.GetAttackableTiles().Contains(tile))
            {
                SelectedUnit.Attack(tile.unit);
                return;
            }

        }
    }

    
}
