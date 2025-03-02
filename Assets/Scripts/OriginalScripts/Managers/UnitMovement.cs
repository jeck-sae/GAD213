using Pixelplacement;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class UnitMovement : Singleton<UnitMovement>
{
    [ReadOnly] public bool isMoving;
    [SerializeField] Transform movementParent;

    public static void MoveUnit(Unit unit, Tile destination)
    {
        if (unit.GetWalkableTiles().Contains(destination))
        {
            Instance.StartCoroutine(Instance.MoveUnitCoroutine(unit, destination));
        }
    }

    IEnumerator MoveUnitCoroutine(Unit unit, Tile destination)
    {
        isMoving = true;

        var path = Pathfinder.FindPath(GridManager.Instance, unit.currentTile, destination);

        unit.currentTile.unit = null;
        unit.currentTile = null;

        transform.position = unit.transform.position;

        unit.transform.parent = movementParent;
        unit.transform.position = movementParent.position;
        
        foreach (var tile in path)
        {
            Tween.Position(transform, tile.transform.position, 0.3f, 0, Tween.EaseLinear);
            yield return Helpers.GetWait(0.3f);
        }

        destination.PlaceUnit(unit);
        isMoving = false;
    }


    
}
