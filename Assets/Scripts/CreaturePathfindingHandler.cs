using System.Collections.Generic;
using UnityEngine;

public class CreaturePathfindingHandler
{
    private readonly Creature creature;

    private LineRenderer lr;

    private readonly Pathfinding pf;
    private List<Vector2> moveVectorList = null;
    int currentPathIndex;

    public CreaturePathfindingHandler(Creature creature)
    {
        this.creature = creature;
        lr = creature.gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        Color color = Color.white;
        color.a = .25f;
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = .1f;
        pf = new Pathfinding(MapManager.instance.GetMap().GetGrid());
    }

    //Move to tile
    public void Move()
    {
        if (moveVectorList != null)
        {
            CreateLineToPosition();

            Vector3 targetPos = moveVectorList[currentPathIndex] + new Vector2(.5f, .5f);
            if (Vector2.Distance(creature.transform.position, targetPos) > .01f)
            {
                Vector3 moveDir = (targetPos - creature.transform.position).normalized;

                creature.transform.position += moveDir * creature.speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;

                if (currentPathIndex >= moveVectorList.Count)
                {
                    StopMoving();
                    lr.positionCount = 0;
                }
            }
        }
    }

    private void CreateLineToPosition()
    {
        lr.positionCount = moveVectorList.Count;
        for (int i = 0; i < moveVectorList.Count; i++)
        {
            lr.SetPosition(i, moveVectorList[i] + new Vector2(.5f, .5f));
        }
    }

    private void StopMoving()
    {
        moveVectorList = null;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        List<Vector2> path = pf.FindPath(MapManager.instance.GetGrid().GetXY(creature.transform.position), targetPosition);

        //Do not update the movelist if target cell is invalid
        if (path == null) return;

        currentPathIndex = 0;
        moveVectorList = path;

        if (moveVectorList != null && moveVectorList.Count > 1)
        {
            moveVectorList.RemoveAt(0);
        }
    }
}
