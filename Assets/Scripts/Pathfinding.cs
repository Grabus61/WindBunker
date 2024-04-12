using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private Grid currentGrid;
    private List<MapTile> openList;
    private List<MapTile> closedList;

    public Pathfinding(Grid grid)
    {
        currentGrid = grid;
    }

    public List<Vector2> FindPath(Vector2 worldStartPos, Vector2 worldEndPos)
    {
        Vector2Int start = currentGrid.GetXY(worldStartPos);
        Vector2Int end = currentGrid.GetXY(worldEndPos);

        List<MapTile> path = FindPath(start.x, start.y, end.x, end.y);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector2> vectorPath = new List<Vector2>();
            foreach (MapTile tile in path)
            {
                vectorPath.Add(new Vector3(tile.x, tile.y));
            }
            return vectorPath;
        }
    }

    public List<MapTile> FindPath(int startX, int startY, int endX, int endY)
    {
        MapTile startTile = currentGrid.GetTile(startX, startY);
        MapTile endTile = currentGrid.GetTile(endX, endY);

        //Check if end tile is invalid
        if (endTile == null) return null;

        openList = new List<MapTile> { startTile }; //currently searching
        closedList = new List<MapTile>(); //already searched

        for (int x = 0; x < currentGrid.GetWidth(); x++)
        {
            for (int y = 0; y < currentGrid.GetHeight(); y++)
            {
                MapTile tile = currentGrid.GetTile(x, y);
                tile.gCost = int.MaxValue;
                tile.CalculateFCost();
                tile.cameFromTile = null;
            }
        }

        startTile.gCost = 0;
        startTile.hCost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();

        while (openList.Count > 0)
        {
            MapTile currentTile = GetLowestFCostTile(openList);
            if (currentTile == endTile)
            {
                // Reached Final
                return CalculatePath(endTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (MapTile neighborTile in GetNeighbors(currentTile))
            {
                if (closedList.Contains(neighborTile)) continue;

                int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neighborTile);
                if (tentativeGCost < neighborTile.gCost)
                {
                    neighborTile.cameFromTile = currentTile;
                    neighborTile.gCost = tentativeGCost;
                    neighborTile.fCost = CalculateDistanceCost(neighborTile, endTile);
                    neighborTile.CalculateFCost();
                }

                if (!openList.Contains(neighborTile))
                {
                    openList.Add(neighborTile);
                }
            }
        }

        //out of nodes of open list - not found
        Debug.LogWarning("Path not found!");
        return null;
    }

    private List<MapTile> GetNeighbors(MapTile currentTile)
    {
        List<MapTile> neighbors = new List<MapTile>();

        if (currentTile.x - 1 >= 0)
        {
            //left
            neighbors.Add(currentGrid.GetTile(currentTile.x - 1, currentTile.y));
            //left top
            if (currentTile.y + 1 < currentGrid.GetHeight())
                neighbors.Add(currentGrid.GetTile(currentTile.x - 1, currentTile.y + 1));
            //left bottom
            if (currentTile.y - 1 >= 0)
                neighbors.Add(currentGrid.GetTile(currentTile.x - 1, currentTile.y - 1));
        }
        if (currentTile.x + 1 < currentGrid.GetWidth())
        {
            //right
            neighbors.Add(currentGrid.GetTile(currentTile.x + 1, currentTile.y));
            //right top
            if (currentTile.y + 1 < currentGrid.GetHeight())
                neighbors.Add(currentGrid.GetTile(currentTile.x + 1, currentTile.y + 1));
            //right bottom
            if (currentTile.y - 1 >= 0)
                neighbors.Add(currentGrid.GetTile(currentTile.x + 1, currentTile.y - 1));
        }
        //top
        if (currentTile.y + 1 < currentGrid.GetHeight())
            neighbors.Add(currentGrid.GetTile(currentTile.x, currentTile.y + 1));
        //down
        if (currentTile.y - 1 >= 0)
            neighbors.Add(currentGrid.GetTile(currentTile.x, currentTile.y - 1));

        return neighbors;
    }

    private List<MapTile> CalculatePath(MapTile endTile)
    {
        List<MapTile> path = new List<MapTile>();

        path.Add(endTile);
        MapTile currentTile = endTile;
        while (currentTile.cameFromTile != null)
        {
            path.Add(currentTile.cameFromTile);
            currentTile = currentTile.cameFromTile;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(MapTile t1, MapTile t2)
    {
        int xDistance = Mathf.Abs(t1.x - t2.x);
        int yDistance = Mathf.Abs(t1.y - t2.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private MapTile GetLowestFCostTile(List<MapTile> tileList)
    {
        MapTile lowestFCostTile = tileList[0];
        for (int i = 1; i < tileList.Count; i++)
        {
            if (tileList[i].fCost < lowestFCostTile.fCost)
            {
                lowestFCostTile = tileList[i];
            }
        }
        return lowestFCostTile;
    }
}
