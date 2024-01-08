using UnityEngine;

public class FindFlowerMission : MonoBehaviour
{
    
    public GameObject bieneObject;
    public GameObject blumeObject;

    public float triggerDistance = 1.0f;
    private bool missionCompleted = false;

    public GameObject nectarMissionObject; // Referenz auf das GameObject mit dem NektarMission-Skript

    private void Update()
    {
        if (bieneObject == null || blumeObject == null)
        {
            Debug.LogError("Biene oder Blume GameObjects sind nicht zugewiesen!");
            return;
        }

        float distance = Vector3.Distance(bieneObject.transform.position, blumeObject.transform.position);
        if (distance < triggerDistance && !missionCompleted)
        {
            StartNectarMission();
        }

        else
        { Debug.Log("Konnte keine Blume finden."); }
    }

    private void StartNectarMission()
    {
        Debug.Log("Biene hat die Blume getroffen!");
        missionCompleted = true;

        if (nectarMissionObject != null)
        {
            NektarMission nectarMission = nectarMissionObject.GetComponent<NektarMission>();
            if (nectarMission != null)
            {
                nectarMission.enabled = true; // Aktiviere die Nektarmission
            }
        }
    }
}