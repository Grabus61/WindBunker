using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [Header("Map Settings")]
    [SerializeField] private Vector2Int mapSize;

    private Map map;

    private void Awake()
    {
        if (instance == null) instance = this;
        map = new Map(mapSize);
    }

    //Getter-Setter
    public Map GetMap()
    {
        return map;
    }

    public Grid GetGrid()
    {
        return map.GetGrid();
    }


    //Gizmos
    private void OnDrawGizmos()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Rect rect = new Rect(i, j, 1, 1);
                Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
            }
        }
    }
}
