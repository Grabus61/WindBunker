using UnityEngine;

public class MapTile
{
    private Grid grid;
    public int x { get; private set; }
    public int y { get; private set; }

    //Pathfinding
    public int gCost;
    public int hCost;
    public int fCost;

    public MapTile cameFromTile;
    //----------------------------

    public MapTile(Grid grid, Vector2Int position)
    {
        this.grid = grid;
        this.x = position.x;
        this.y = position.y;
    }

    //Pathfinding methods
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    //Getter-setter
}
