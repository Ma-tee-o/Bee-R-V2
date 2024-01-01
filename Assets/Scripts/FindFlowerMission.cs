using UnityEngine;

public class FindFlowerMission : MonoBehaviour

{
    public MissionManager missionManager; 
    public string flowerTag = "Blume"; 
    public NektarMission nectarMission; // Referenz auf die nächste Mission

    private bool missionCompleted = false; 

    private void Start()
    {
        enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!missionCompleted && other.CompareTag("Biene"))
        {
            if (other.CompareTag(flowerTag))
            {
                BlumeGefunden();
            }
        }
    }

    void BlumeGefunden()
    {
        missionCompleted = true;
        missionManager.FindFlower();

        if (nectarMission != null)
        {
            nectarMission.enabled = true; // Aktiviere die nächste Mission
        }
        
    }
}

