using UnityEngine;
using System.Collections.Generic;

public class FindFlowerMission : MonoBehaviour
{
    public GameObject bieneObject;
    public List<GameObject> blumeObjects = new List<GameObject>();
    public GameObject pollenMissionObject; // Referenz auf das GameObject mit dem PollenMission-Skript

    public float triggerDistance = 25.0f;
    private bool missionCompleted = false;

    private void Update()
    {
        if (bieneObject == null || blumeObjects.Count == 0)
        {
            Debug.LogError("Biene oder Blume GameObjects sind nicht zugewiesen!");
            return;
        }

        foreach (GameObject blumeObject in blumeObjects)
        {
            float distance = Vector3.Distance(bieneObject.transform.position, blumeObject.transform.position);
            if (distance < triggerDistance && !missionCompleted)
            {
                // Starte keine nicht vorhandene Methode mehr
                missionCompleted = true;
                Debug.Log("Biene hat eine Blume getroffen!");
                break;
            }
        }

        if (!missionCompleted)
        {
            Debug.Log("Konnte keine Blume finden.");
        }
    }
}
