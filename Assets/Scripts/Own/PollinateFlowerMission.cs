using UnityEngine;
using UnityEngine.InputSystem;

public class PollinateFlowerMission : MonoBehaviour
{
    public float requiredPollinationTime = 5f;
    public float currentPollinationTime = 0f;
    public int pollinationPoints = 0;
<<<<<<< HEAD
    public float triggerDistance = 5.0f;
=======
    public float triggerDistance = 1.0f;
>>>>>>> 9fe6949ec5f1aa664d278e8553c248cbe3f3ce01
    public InputActionProperty bestaubenbutton;
    private bool isPollinating = false;
    private bool[] hasBeenPollinated;
    private bool[] isFlowerActivated; // Neue Variable, um den Aktivierungsstatus zu speichern

    public GameObject bieneObject;
    public GameObject[] blumeObjects;

    void Start()
    {
        hasBeenPollinated = new bool[blumeObjects.Length];
        isFlowerActivated = new bool[blumeObjects.Length]; // Initialisiere die neue Variable

        for (int i = 0; i < hasBeenPollinated.Length; i++)
        {
            hasBeenPollinated[i] = false;
            isFlowerActivated[i] = false; // Setze den Aktivierungsstatus auf false
        }

        if (bieneObject == null || blumeObjects.Length == 0)
        {
            Debug.LogError("Biene- oder Blumen-GameObject nicht zugewiesen!");
        }

        // Registriere den Callback für den Bestäubungs-Button
        bestaubenbutton.action.performed += OnBestaubenButtonPressed;
    }

    void Update()
    {
        if (isPollinating)
        {
            currentPollinationTime += Time.deltaTime;

            if (currentPollinationTime >= requiredPollinationTime)
            {
                CompletePollination();
            }
        }
    }

    void OnBestaubenButtonPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
        {
            // Überprüfe die Distanz zwischen der Biene und den Blumen
            for (int i = 0; i < blumeObjects.Length; i++)
            {
                float distance = Vector3.Distance(bieneObject.transform.position, blumeObjects[i].transform.position);

                // Überprüfe, ob die Biene eine Blume erreicht hat, die noch nicht bestäubt wurde
                // und die Blume nicht bereits aktiviert ist
                if (distance < triggerDistance && !isPollinating && !hasBeenPollinated[i] && !isFlowerActivated[i])
                {
                    StartPollination(i); // Übergebe den Index der aktuellen Blume
                }
            }
        }
        else
        {
            // Stoppe die Bestäubung, wenn der Button losgelassen wird
            StopPollination();
        }
    }

    void StartPollination(int flowerIndex)
    {
        isFlowerActivated[flowerIndex] = true; // Setze den Aktivierungsstatus auf true
        isPollinating = true;
        Debug.Log("Bestäubung gestartet an Blume #" + flowerIndex + ": " + blumeObjects[flowerIndex].name);
    }

    void StopPollination()
    {
        isPollinating = false;
        currentPollinationTime = 0f;
        Debug.Log("Bestäubung gestoppt.");
    }

    void CompletePollination()
    {
        isPollinating = false;
        currentPollinationTime = 0f;

        // Markiere die aktuelle Blume als bestäubt
        for (int i = 0; i < blumeObjects.Length; i++)
        {
            float distance = Vector3.Distance(bieneObject.transform.position, blumeObjects[i].transform.position);

            // Überprüfe, ob die Biene in der Nähe der Blume ist, die noch nicht bestäubt wurde
            // und die Blume aktiviert wurde
            if (distance < triggerDistance && !hasBeenPollinated[i] && isFlowerActivated[i])
            {
                hasBeenPollinated[i] = true;
                pollinationPoints++; // Erhöhe die Punktzahl nur, wenn die Blume noch nicht bestäubt wurde
                Debug.Log("Bestäubung abgeschlossen! Punktzahl: " + pollinationPoints + " an Blume #" + i + ": " + blumeObjects[i].name);
            }
        }

        // Führe hier die Aktionen für die abgeschlossene Bestäubung aus
    }
}
