using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections.Generic;

public class PollenCollect : MonoBehaviour
{
    public PollinateFlowerMission pollinateFlowerMission;
    public XRSocketInteractor socketInteractor;
    public List<GameObject> pollenObjects = new List<GameObject>();
    public TextMeshProUGUI pollenAttachedText; // TextMeshProUGUI-Objekt für den Text, wenn der Pollen angebracht ist

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

                    // Anzeige des Textfelds, wenn der Pollen angebracht ist
                    if (pollenAttachedText != null)
                    {
                        pollenAttachedText.text = "Mission 3: PollinateFlowers: Touch all 9 Flowers and then hold the X-Button on the left Controller till the Pollination-Score is shown";

                        // Rufe die Methode ClearDisplayText nach 6 Sekunden auf, um den Text zu löschen
                        Invoke("ClearDisplayText", 6f);
                    }

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

    void ClearDisplayText()
    {
        if (pollenAttachedText != null)
        {
            pollenAttachedText.text = "";
        }
    }
}
