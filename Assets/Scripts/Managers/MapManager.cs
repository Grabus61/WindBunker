using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [Header("Map Settings")]
    [SerializeField] private bool drawGizmosGrid;
    [SerializeField] private Vector2Int mapSize;

    [Header("References")]
    [SerializeField] private Transform gridVisuals;

    private Map map;

    private void Awake()
    {
        if (instance == null) instance = this;
        map = new Map(mapSize);

        gridVisuals.localScale = (Vector2)mapSize;
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

    public Vector2 GetSize()
    {
        return mapSize;
    }


    //Gizmos
    private void OnDrawGizmos()
    {
        if (drawGizmosGrid)
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
}
