using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public float spawnDistance;
    public GameObject menu;
    public InputActionProperty showMenuButton;

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

        if (showMenuButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            if (menu.activeSelf)
            {
                menu.transform.position = activeCamera.transform.position +
                    new Vector3(activeCamera.transform.forward.x, 0, activeCamera.transform.forward.z).normalized * spawnDistance;

                relativePosition = new Vector3(menu.transform.position.x - activeCamera.transform.position.x,
                    0, menu.transform.position.z - activeCamera.transform.position.z);
            }
        }

        menu.transform.position = activeCamera.transform.position + relativePosition;

        menu.transform.LookAt(new Vector3(activeCamera.transform.position.x, menu.transform.position.y, activeCamera.transform.position.z));
        menu.transform.forward *= -1;
    }

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
