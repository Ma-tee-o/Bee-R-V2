using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public Text missionTextDisplay; // UI-Textobjekt für die Missionen-Anzeige, die dinger noch erstellen
    private int foundFlowers = 0;
    private int collectedNectar = 0;
    private int pollinatedFlowers = 0;

    

    private void UpdateMissionText()
    {
        string missionText = "Missionen Fortschritt:\n";
        missionText += "Blume gefunden: " + foundFlowers.ToString() + "/1\n";
        missionText += "Nektar gesammelt: " + collectedNectar.ToString() + "/3\n";
        missionText += "Blume bestäubt: " + pollinatedFlowers.ToString() + "/1";

        missionTextDisplay.text = missionText;
    }

    public void FindFlower()
    {
        foundFlowers++;
        UpdateMissionText();

        if (foundFlowers == 1)
        {
            Debug.Log("Mission 'Blume finden' abgeschlossen!");
            
        }
    }

    public void CollectNectar()
    {
        collectedNectar++;
        UpdateMissionText();

        if (collectedNectar == 3)
        {
            Debug.Log("Mission 'Nektar sammeln' abgeschlossen!");
        }            
    }

    public void PollinateFlower()
    {
        if (pollinatedFlowers == 1)
        {
            Debug.Log("Mission 'Blume bestäuben' abgeschlossen!");
           
        }
    }

 }   