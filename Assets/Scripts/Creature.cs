using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class Creature : MonoBehaviour, IMoveable
{
    public int age;
    public float speed;

    private CreaturePathfindingHandler pfHandler;

    private void Awake()
    {
        pfHandler = new CreaturePathfindingHandler(this);
    }

    private void Update()
    {
        pfHandler.Move();
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        pfHandler.SetTargetPosition(targetPosition);
    }
}
