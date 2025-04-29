using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonMap : MonoBehaviour
{
    public MapSelection currentWaypoint;
    public float defaultCameraSize = 10f;
    public Vector3 defaultCameraPosition;

    public void OnMouseDown()
    {
        currentWaypoint.ZoomOut(defaultCameraSize, defaultCameraPosition);
    }
}
