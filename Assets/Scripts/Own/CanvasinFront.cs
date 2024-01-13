using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasinFront : MonoBehaviour
{
    public float spawnDistance;
    [SerializeField] private GameObject Canvas;

    private Vector3 relativePosition;

    void Update()
    {
        // Find the active camera dynamically
        Camera activeCamera = FindActiveCamera();

        if (activeCamera == null)
        {
            Debug.LogError("No active camera found.");
            return;
        }

        if (Canvas.activeSelf)
        {
            Canvas.transform.position = activeCamera.transform.position +
                new Vector3(activeCamera.transform.forward.x, 0, activeCamera.transform.forward.z).normalized * spawnDistance;

            relativePosition = new Vector3(Canvas.transform.position.x - activeCamera.transform.position.x,
                0, Canvas.transform.position.z - activeCamera.transform.position.z);
        }


        Canvas.transform.position = activeCamera.transform.position + relativePosition;

        Canvas.transform.LookAt(new Vector3(activeCamera.transform.position.x, Canvas.transform.position.y, activeCamera.transform.position.z));
        Canvas.transform.forward *= -1;



        // Function to find the active camera dynamically
        Camera FindActiveCamera()
        {
            Camera[] cameras = Camera.allCameras;

            foreach (Camera cam in cameras)
            {
                if (cam.isActiveAndEnabled)
                {
                    return cam;
                }
            }

            return null;
        }
    }
}
    