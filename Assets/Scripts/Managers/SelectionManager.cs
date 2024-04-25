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
            Select(hit);
        }
        else
        {
            //Deselect
            selectedObj = null;
            selector.transform.SetParent(transform);
            selector.SetActive(false);
        }
    }

    //Select the clicked object
    private void Select(RaycastHit2D hit)
    {
        GameObject hitObj = hit.collider.gameObject;
        Debug.Log(hitObj.name);
        selectedObj = hitObj; //Will changed to an array of objects for multi-select pusposes

        SetSelector(hitObj);
    }

    //Set the selector position for movable and non-movable objects
    private void SetSelector(GameObject hitObj)
    {
        selector.SetActive(true);

        if (hitObj.GetComponent<IMoveable>() != null)
        {
            selector.transform.SetParent(hitObj.transform);
            selector.transform.position = hitObj.transform.position;
        }
        else
        {
            selector.transform.SetParent(transform);
            selector.transform.position = MapManager.instance.GetMap().GetGrid().GetXY(hitObj.transform.position) + new Vector2(0.5f, 0.5f);
        }
    }
}
