using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    [Header("Zoom Settings")]
    public Transform zoomTargetPoint; // <- Ini posisi tengah/pusat kamera
    public GameObject levelPanel;
    public float zoomSize = 5f;
    public float zoomDuration = 0.5f;

    private bool isZoomedIn = false;

    // Simpan posisi awal camera
    private Vector3 defaultCameraPos;
    private float defaultCameraSize;
    private CameraMovement cameraMovement;


    void Start()
    {
        defaultCameraPos = Camera.main.transform.position;
        defaultCameraSize = Camera.main.orthographicSize;

        cameraMovement = Camera.main.GetComponent<CameraMovement>(); // Cari CameraMovement
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                Debug.Log("Touched or clicked " + gameObject.name);
                // Lanjutkan aksi zoom atau panel

                if (isZoomedIn || Camera.main == null || zoomTargetPoint == null) return;

                Vector3 targetPos = new Vector3(
                    zoomTargetPoint.position.x,
                    zoomTargetPoint.position.y,
                    Camera.main.transform.position.z
                );

                StartCoroutine(CameraZoomIn(targetPos));
            }
        }
    }


    System.Collections.IEnumerator CameraZoomIn(Vector3 targetPos)
    {
        isZoomedIn = true;
        if (cameraMovement != null)
            cameraMovement.canDrag = false; // Kunci drag saat zoom in

        Camera cam = Camera.main;
        float startSize = cam.orthographicSize;
        Vector3 startPos = cam.transform.position;
        float t = 0;

        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float lerp = t / zoomDuration;
            cam.orthographicSize = Mathf.Lerp(startSize, zoomSize, lerp);
            cam.transform.position = Vector3.Lerp(startPos, targetPos, lerp);
            yield return null;
        }

        levelPanel.SetActive(true);
    }

    public void ZoomOut(float defaultSize, Vector3 defaultPos)
    {
        StartCoroutine(ZoomBack(defaultSize, defaultPos));
    }

    IEnumerator ZoomBack(float size, Vector3 pos)
    {
        Camera cam = Camera.main;
        float startSize = cam.orthographicSize;
        Vector3 startPos = cam.transform.position;
        float t = 0;

        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float lerp = t / zoomDuration;
            cam.orthographicSize = Mathf.Lerp(startSize, size, lerp);
            cam.transform.position = Vector3.Lerp(startPos, pos, lerp);
            yield return null;
        }

        levelPanel.SetActive(false);
        isZoomedIn = false;

        if (cameraMovement != null)
            cameraMovement.canDrag = true; // Balikin drag saat zoom out
    }

}
