using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PathVisualizer : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform targetPoint;
    private LineRenderer lineRenderer;
    private NavMeshPath path;
    public float updateInterval = 2f; // Setiap berapa detik jalur diupdate

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        path = new NavMeshPath();
        StartCoroutine(UpdatePathRoutine());
    }

    IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval);
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        if (spawnPoint == null || targetPoint == null) return;

        if (NavMesh.CalculatePath(spawnPoint.position, targetPoint.position, NavMesh.AllAreas, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }
}
