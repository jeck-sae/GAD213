using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [ShowInInspector] Dictionary<Vector2Int, Tile> tiles;

    public bool isSetup {  get; protected set; }
    protected void Awake()
    {
        if (isSetup) return;
        tiles = new Dictionary<Vector2Int, Tile>();
        BroadcastMessage("SetupTile");
        isSetup = true;
    }


    public void AddTile(Vector2Int position, Tile tile)
    {
        if (tiles.ContainsKey(position))
        {
            Debug.Log("the slot " + position + " is already occupied");
            return;
        }

        tiles[position] = tile;
        tile.position = position;
    }

    public static Vector2Int FixCoordinates(Vector2 position) => new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    public Tile Get(Vector3 position) => Get(FixCoordinates(position));
    public Tile Get(Vector2Int position)
    {
        if (tiles.ContainsKey(position))
            return tiles[position];
        return null;
    }

    public Dictionary<Vector2Int, Tile> GetAll() => tiles;

    public void Remove(Tile tile)
    {
        tiles?.Remove(tiles.FirstOrDefault(x => x.Value == tile).Key);
    }
    public void Remove(Vector2Int position)
    {
        tiles?.Remove(position);
    }

    public bool Contains(Vector2Int position) => tiles.ContainsKey(position);
    public bool Contains(Tile tile) => tiles.ContainsValue(tile);


    public List<Tile> GetAdjacentTiles(Vector2Int position)
    {
        List<Tile> neighbors = new List<Tile>();

        if (tiles.TryGetValue(position + Vector2Int.right, out Tile right))
            neighbors.Add(right);
        if (tiles.TryGetValue(position + Vector2Int.left, out Tile left))
            neighbors.Add(left);
        if (tiles.TryGetValue(position + Vector2Int.up, out Tile up))
            neighbors.Add(up);
        if (tiles.TryGetValue(position + Vector2Int.down, out Tile down))
            neighbors.Add(down);

        return neighbors;
    }
    
    public List<Tile> GetTilesInRange(Vector2Int position, int range)
    {
        List<Tile> list = new List<Tile>();
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                int num = Mathf.Abs(i);
                int num2 = Mathf.Abs(j);
                if (num + num2 <= range)
                {
                    var pos = new Vector2Int(position.x + i, position.y + j);
                    if (Contains(pos))
                        list.Add(Get(pos));
                }
            }
        }
        return list;
    }

    public List<Tile> GetTilesInCross(Vector2Int position, int range) 
    { 
        List<Tile> list = new List<Tile>();
        for (int i = -range; i <= range; i++)
        {
            var pos = new Vector2Int(position.x + i, position.y);
            if (Contains(pos))
                list.Add(Get(pos));
        }
        for (int i = -range; i <= range; i++)
        {
            var pos = new Vector2Int(position.x, position.y + i);
            if (Contains(pos))
                list.Add(Get(pos));
        }
        return list;
    }

    public List<Tile> GetTilesInLine(Vector2Int startingTile, Vector2Int direction, int range, Func<Tile, bool> stopWhenFalse = null)
    {
        List<Tile> tilesInLine = new List<Tile>();
        for (int i = 0; i < range; i++)
        {
            var tile = Get(startingTile + direction * i);
            
            if (stopWhenFalse != null && stopWhenFalse(tile) == false)
                break;

            tilesInLine.Add(tile);
        }
        return tilesInLine;
    }
}