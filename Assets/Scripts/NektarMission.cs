using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using System.Collections.Generic;

public class NektarMission : MonoBehaviour
{
    public MissionManager missionManager;
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    public InputDevice targetDevice;

    public GameObject dropZoneObject; 
    public PollinateFlowerMission pollinateFlowerMission; // Referenz auf die nächste Mission

    private bool nectarPickedUp = false; 
    private Vector3 originalPosition; 
    private Coroutine resetNectarCoroutine; 
    private int nectarDroppedCount = 0; 

    private void Start()
    {
        originalPosition = transform.position; 
        StartResetTimer(); 
    }

    private void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        foreach (var item in devices)
        {
            targetDevice = devices[0];
        }
  
}
    private void FixedUpdate()
    {
    if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool triggerValue) && triggerValue)
        {
            if (!nectarPickedUp)
            {
                PickupNectar(); 
            }
            else
            {
                DropNectar(); 
            }
        }

        if (BeeIsNearNectar() && targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            if (!nectarPickedUp)
            {
                PickupNectar(); 
            }
            else
            {
                DropNectar(); 
            }
        }
    }

    void PickupNectar()
    {
        GameObject[] nectarObjects = GameObject.FindGameObjectsWithTag("Nectar");
        if (nectarObjects.Length > 0)
        {
            // Hier kannst du die Logik implementieren, um den Nektar aufzuheben
            // In diesem Beispiel wird der erste gefundene Nektar aufgehoben
            transform.position = nectarObjects[0].transform.position;
            nectarPickedUp = true;
            StopResetTimer();
        }
        else
        {
            Debug.LogWarning("Kein Nektar gefunden!");
        }
    }

    void DropNectar()
    {
        GameObject[] dropZoneObjects = GameObject.FindGameObjectsWithTag("Dropzone");
        if (dropZoneObjects.Length > 0)
        {
            transform.position = dropZoneObjects[0].transform.position;
            nectarPickedUp = false;
            StartResetTimer();

            if (IsNectarInDropZone())
            {
                nectarDroppedCount++;
                if (nectarDroppedCount >= 3)
                {
                    missionManager.CollectNectar(); 
                    nectarDroppedCount = 0; 
                }
            }
        }
        else
        {
            Debug.LogWarning("Keine Dropzone gefunden!");
        }
    }

    bool BeeIsNearNectar()
    {
        GameObject beeObject = GameObject.FindGameObjectWithTag("Biene");
        if (beeObject != null)
        {
            float distance = Vector3.Distance(transform.position, beeObject.transform.position);
            float threshold = 1.0f; 
            return distance < threshold;
        }
        return false;
    }

    bool IsNectarInDropZone()
    {
        GameObject[] dropZoneObjects = GameObject.FindGameObjectsWithTag("Dropzone");
        if (dropZoneObjects.Length > 0)
        {
            float distance = Vector3.Distance(transform.position, dropZoneObjects[0].transform.position);
            float threshold = 1.0f; 
            return distance < threshold;
        }
        return false;
    }

    public void StartResetTimer()
    {
        resetNectarCoroutine = StartCoroutine(ResetNectarAfterDelay(60f)); 
    }

    void StopResetTimer()
    {
        if (resetNectarCoroutine != null)
        {
            StopCoroutine(resetNectarCoroutine); 
        }
    }

   IEnumerator ResetNectarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        transform.position = originalPosition;
        Debug.Log("Nektar zurückgesetzt!");
    }
void ActivateNextMission()
    {
        if (pollinateFlowerMission != null)
        {
            pollinateFlowerMission.enabled = true; // Aktiviere die nächste Mission
        }
    }
}