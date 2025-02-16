using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class TileDisplayManager : MonoBehaviour
{
    [SerializeField] Color SelectedTileColor;
    [SerializeField] Color movementRangeColor;
    [SerializeField] Color confirmHoveringTileColor;
    [SerializeField] Color errorHoveringTileColor;
    [SerializeField] Color attackRangeColor;
    [SerializeField] Color enemyTileColor;
    [SerializeField] Color allyTileColor;



    void Update()
    {
        //clear path preview
        GridManager.Instance.GetAll().ForEach(x => x.Value.gfx.ResetGraphics());

        ColorUnits();

        PreviewSelecetdUnit();
    }

    void PreviewSelecetdUnit()
    {
        Unit selectedUnit = UnitManager.Instance.SelectedUnit;
        Tile selectedTile = UnitManager.Instance.SelectedTile;

        if(!selectedUnit || !selectedTile)
            return;

        selectedTile.gfx.SetOuterBorderColor(SelectedTileColor);
        PreviewMovementRange(selectedUnit);

        Tile hovering = null; 
        if(!Helpers.IsOverUI)
            hovering = GridManager.Instance.Get(Helpers.Camera.ScreenToWorldPoint(Input.mousePosition));

        if (!hovering)
        {
            if (selectedTile)
                PreviewAttack(selectedTile);
            return;
        }

        var walkable = selectedUnit.GetWalkableTiles();
        
        //Walk preview
        if (walkable.Contains(hovering))
        {
            var path = Pathfinder.FindPath(GridManager.Instance, selectedTile, hovering);
            path.Insert(0, selectedTile);
            PreviewPath(path);
            hovering.gfx.SetFillColor(confirmHoveringTileColor);
            hovering.gfx.SetBorderColor(confirmHoveringTileColor);
            
            PreviewAttack(hovering);
            return;
        }

        PreviewAttack(selectedTile);

        var attackable = selectedUnit.GetAttackableTiles();
        
        //hovering attackable enemy
        if (hovering.unit && hovering.unit.isEnemy && attackable.Contains(hovering))
        {
            hovering.gfx.SetBorderColor(new Color(0, 0, 0, 0));
            hovering.gfx.SetOuterBorderColor(errorHoveringTileColor);
        }
        //hovering uninteractable slot
        else if (hovering != selectedTile && hovering.unit == null)
        { 
            hovering.gfx.SetFillColor(errorHoveringTileColor);
            hovering.gfx.SetBorderColor(errorHoveringTileColor);
        }

    }


    void ColorUnits()
    {
        foreach (var unit in UnitManager.Instance.Units)
        {
            if (unit.isEnemy)
                unit.currentTile?.gfx.SetBorderColor(enemyTileColor);
            else 
                unit.currentTile?.gfx.SetBorderColor(allyTileColor);
        }
    }


    void PreviewAttack(Tile center)
    {
        var attackRangeTiles = UnitManager.Instance.SelectedUnit.GetAttackableTiles(center);
        attackRangeTiles.ForEach(x => x.gfx.SetInnerBorderColor(attackRangeColor));
    }

    void PreviewPath(List<Tile> path)
    {
        for (int i = 1; i < path.Count - 1; i++)
        {
            Vector2Int previous = path[Mathf.Max(i - 1, 0)].position - path[i].position;
            Vector2Int next = path[Mathf.Min(i + 1, path.Count - 1)].position - path[i].position;

            path[i].gfx.SetPathPreview(previous, next);
        }
    }

    void PreviewMovementRange(Unit unit)
    {
        var tiles = unit.GetWalkableTiles();
        tiles.ForEach(x => x.gfx.SetBorderColor(movementRangeColor));
    }
}
