using UnityEngine;
public class PollinateFlowerMission : MonoBehaviour
{
    public MissionManager missionManager; // Referenz zum MissionManager-Skript

    private bool missionCompleted = false; // Hinzugefügte Variable zur Überprüfung der Missionserfüllung

    // Annahme: Die Funktion zum Schütteln des Controllers wird hier aufgerufen
    void ShakeController()
    {
        if (!missionCompleted && BeeIsNearFlower()) // Überprüfe, ob die Mission nicht bereits erfüllt wurde und die Biene sich an der Blume befindet
        {
            FlowerPollinated(); // Blume wurde bestäubt
        }
    }

    bool BeeIsNearFlower()
{
    GameObject beeObject = GameObject.FindGameObjectWithTag("Biene");
    GameObject flowerObject = GameObject.FindGameObjectWithTag("Blume");

    if (beeObject != null && flowerObject != null)
    {
        float distance = Vector3.Distance(beeObject.transform.position, flowerObject.transform.position);
        float threshold = 1.0f; // Beispielhafter Schwellenwert für die Entfernung

        return distance < threshold;
    }
    return false;
}


    void FlowerPollinated()
    {
        missionCompleted = true; // Markiere die Mission als erfüllt
        missionManager.PollinateFlower(); // Rufe die Methode im MissionManager-Skript auf
        
        // Weitere Aktionen für das Bestäuben der Blume hier (z.B. Animationen, Soundeffekte usw.)
    }
}