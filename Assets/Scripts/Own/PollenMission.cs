using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class PollenMission : MonoBehaviour
{
    public PollinateFlowerMission pollinateFlowerMission;
    public XRSocketInteractor socketInteractor;
    public List<GameObject> pollenObjects = new List<GameObject>();

    private bool missionCompleted = false;

    void Start()
    {
        Debug.Log("PollenMission aktiviert!");
    }

    void Update()
    {
        // Überprüfe fortlaufend, ob ein Pollen-Objekt am Attach-Punkt ist
        foreach (GameObject pollenObject in pollenObjects)
        {
            if (IsPollenAttachedToSocket(pollenObject))
            {
                if (!missionCompleted)
                {
                    missionCompleted = true;
                    Debug.Log("Pollen-Objekt wurde an den Sockel angebracht!");

                    // Hier könntest du weitere Aktionen nach Abschluss der Mission durchführen
                }
            }
        }
    }

    bool IsPollenAttachedToSocket(GameObject pollenObject)
    {
        if (socketInteractor != null && pollenObject != null)
        {
            // Überprüfe, ob ein Pollen-Objekt an den Koordinaten des Attach-Punkts ist
            float distance = Vector3.Distance(pollenObject.transform.position, socketInteractor.attachTransform.position);
            return distance < 0.9f;
        }

        return false;
    }
}
