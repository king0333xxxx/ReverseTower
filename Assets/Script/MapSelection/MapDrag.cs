using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrag : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Vector3 startPosition;

    public float dragSpeed = 1f;
    public float minX, maxX, minY, maxY; // Batas gerak map (opsional)

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Bounds bounds = sr.bounds;

            minX = bounds.min.x;
            maxX = bounds.max.x;
            minY = bounds.min.y;
            maxY = bounds.max.y;
        }
    }

    void Update()
    {
        // Untuk mouse
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragOrigin;
            Vector3 newPos = startPosition - difference * dragSpeed;

            // Batasan area drag (opsional)
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.position = newPos;
        }

        // Untuk touch (mobile)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
                startPosition = transform.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(touch.position) - dragOrigin;
                Vector3 newPos = startPosition - difference * dragSpeed;

                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
                newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

                transform.position = newPos;
            }
        }
    }
}
