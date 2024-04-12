using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectedObj = null; //has to be selectable

    [SerializeField] private GameObject selector;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckClickedObject();
        }
        if (selectedObj)
        {
            if (selectedObj.GetComponent<Creature>() && Mouse.current.rightButton.wasPressedThisFrame)
            {
                selectedObj.GetComponent<Creature>().SetTargetPosition(MapManager.instance.GetGrid().GetMouseGridPosition());
            }
        }
    }

    private void CheckClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.GetComponent<Selectable>())
        {
            GameObject hitObj = hit.collider.gameObject;
            Debug.Log(hitObj.name);
            selectedObj = hitObj;
            selector.SetActive(true);
            if (hit.collider.GetComponent<IMoveable>() != null)
            {
                selector.transform.SetParent(hitObj.transform);
                selector.transform.position = hitObj.transform.position;
            }
            else
            {
                selector.transform.SetParent(transform);
                selector.transform.position = MapManager.instance.GetMap().GetGrid().GetXY(hit.collider.transform.position) + new Vector2(0.5f, 0.5f);
            }
        }
        else
        {
            selector.transform.SetParent(transform);
            selectedObj = null;
            selector.SetActive(false);
        }
    }
}
