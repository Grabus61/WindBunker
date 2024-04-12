using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grid
{
    private MapTile[,] tiles;
    Vector2Int size;

    public Grid(Vector2Int size)
    {
        this.size = size;

        tiles = new MapTile[size.x, size.y];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int tilePos = new(x, y);
                tiles[x, y] = new MapTile(this, tilePos);
            }
        }
    }

    public Vector2Int GetMouseGridPosition()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int gridPos = new((int)Mathf.Floor(mouseWorldPos.x), (int)Mathf.Floor(mouseWorldPos.y));
        return gridPos;
    }

    public Vector2Int GetXY(Vector2 reference)
    {
        Vector2Int gridPos = new((int)reference.x, (int)reference.y);
        return gridPos;
    }

    public MapTile GetTile(int x, int y)
    {
        try
        {
            if (tiles[x, y] != null) return tiles[x, y];
        }
        catch (Exception e)
        {
            return null;
        }
        return null;
    }

    public Vector2Int GetTilePosition(MapTile tile)
    {
        return new Vector2Int(tile.x, tile.y);
    }

    public int GetWidth()
    {
        return tiles.GetLength(0);
    }

    public int GetHeight()
    {
        return tiles.GetLength(1);
    }
}
