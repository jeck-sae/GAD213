using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int position;
    public bool IsWalkable = true;
    public TileGFX gfx;

    [SerializeField] protected Transform contentParent;
    public Unit unit;
    
    private void Awake()
    {
        if(contentParent == null)
            contentParent = transform.Find("Content");
        gfx = GetComponent<TileGFX>();
        if (GridManager.Instance.isSetup)
            SetupTile();
    }

    protected virtual void SetupTile()
    {
        UpdatePosition();
    }

    private void OnDestroy()
    {
        GridManager.Instance?.Remove(this);
    }

    public void PlaceUnit(Unit unit)
    {
        if (unit == null) return;
        if (this.unit != null && this.unit != unit)
        {
            Debug.LogError("Tile is already occupied!");
            return;
        }

        this.unit = unit;
        this.unit.transform.parent = contentParent;
        this.unit.transform.position = contentParent.position;
        unit.currentTile = this;
    }

    [HideInPlayMode, Button("Update Tile Position")]
    private void UpdatePositionOutsidePlayMode()
    {
        position = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    [HideInEditorMode, Button("Update Tile Position")]
    protected void UpdatePosition()
    {
        if (GridManager.Instance.Contains(this))
            GridManager.Instance.Remove(this);

        position = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y));

        if (GridManager.Instance.Contains(position))
        {
            Debug.Log($"[{position.x}, {position.y}] Tile is occupied!");
            return;
        }

        transform.position = new Vector3(position.x, position.y, transform.position.z);

        GridManager.Instance.AddTile(position, this);
    }

}

