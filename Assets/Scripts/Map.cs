using UnityEngine;

public class Map
{
    //Settings
    private Vector2Int size;

    private Grid grid;

    //Constructor
    public Map(Vector2Int size)
    {
        grid = new Grid(size);
        this.size = size;
    }

    public Grid GetGrid()
    {
        return grid;
    }
}
