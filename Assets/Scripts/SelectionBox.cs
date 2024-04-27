using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    private SpriteRenderer renderer;

    Vector2 startPos;
    Vector2 endPos;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            renderer.enabled = true;
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawBox();
        }
        if (Input.GetMouseButtonUp(0))
        {
            renderer.enabled = false;
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void DrawBox()
    {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPos;

        Vector2 boxCenter = (endPos + startPos) / 2;
        transform.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxEnd.x - boxStart.x), Mathf.Abs(boxEnd.y - boxStart.y));
        renderer.size = boxSize;
    }
}
