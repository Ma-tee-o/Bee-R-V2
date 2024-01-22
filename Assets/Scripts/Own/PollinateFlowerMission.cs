using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

public class PollinateFlowerMission : MonoBehaviour
{
    public UnityEvent allpollinated;
    public float requiredPollinationTime = 3f;
    public float displayDuration = 13f;
    private float currentPollinationTime = 0f;
    private float currentDisplayTime = 0f;
    public int pollinationPoints = 0;
    private float triggerDistance = 20.0f;
    public InputActionProperty bestaubenbutton;
    private bool isPollinating = false;
    private bool[] hasBeenPollinated;
    private bool[] isFlowerActivated;

    public GameObject bieneObject;
    public GameObject[] blumeObjects;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missionCompleteText; // TextMeshProUGUI-Objekt für den Missionsabschlusstext

    private int totalPollinated = 0;
    private bool displayActive = false;

    public float TriggerDistance { get => triggerDistance; set => triggerDistance = value; }
    public bool IsPollinating { get => isPollinating; set => isPollinating = value; }

    void Start()
    {
        hasBeenPollinated = new bool[blumeObjects.Length];
        isFlowerActivated = new bool[blumeObjects.Length];

        for (int i = 0; i < hasBeenPollinated.Length; i++)
        {
            hasBeenPollinated[i] = false;
            isFlowerActivated[i] = false;
        }

        if (bieneObject == null || blumeObjects.Length == 0)
        {
            Debug.LogError("Biene- oder Blumen-GameObject nicht zugewiesen!");
        }

        bestaubenbutton.action.performed += OnBestaubenButtonPressed;
    }

    void Update()
    {
        if (IsPollinating)
        {
            currentDisplayTime += Time.deltaTime;

            if (currentDisplayTime >= displayDuration && displayActive)
            {
                ClearDisplayText();
                displayActive = false;
            }

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
            for (int i = 0; i < blumeObjects.Length; i++)
            {
                float distance = Vector3.Distance(bieneObject.transform.position, blumeObjects[i].transform.position);

                if (distance < TriggerDistance && !IsPollinating && !hasBeenPollinated[i] && !isFlowerActivated[i])
                {
                    StartPollination(i);
                }
            }
        }
        else
        {
            StopPollination();
        }
    }

    void StartPollination(int flowerIndex)
    {
        isFlowerActivated[flowerIndex] = true;
        IsPollinating = true;
        Debug.Log("Bestäubung gestartet an Blume #" + flowerIndex + ": " + blumeObjects[flowerIndex].name);
    }

    void StopPollination()
    {
        IsPollinating = false;
        currentPollinationTime = 0f;
        Debug.Log("Bestäubung gestoppt.");
    }

    void CompletePollination()
    {
        IsPollinating = false;
        currentPollinationTime = 0f;
        currentDisplayTime = 0f;

        for (int i = 0; i < blumeObjects.Length; i++)
        {
            float distance = Vector3.Distance(bieneObject.transform.position, blumeObjects[i].transform.position);

            if (distance < TriggerDistance && !hasBeenPollinated[i] && isFlowerActivated[i])
            {
                hasBeenPollinated[i] = true;
                totalPollinated++;
                pollinationPoints++;
                Debug.Log("Bestäubung abgeschlossen an Blume #" + i + ": " + blumeObjects[i].name);

                if (scoreText != null && !displayActive)
                {
                    displayActive = true;
                    scoreText.text = "Flowers pollinated: " + pollinationPoints;
                    Invoke("ClearDisplayText", displayDuration);
                }
            }
        }

        if (totalPollinated == blumeObjects.Length)
        {
            Debug.Log("Bestäuben-Mission erfolgreich!");
            allpollinated.Invoke();
            DisplayMissionCompleteText(); // Neue Methode für Missionsabschlusstext
        }
    }

    void ClearDisplayText()
    {
        scoreText.text = "";
        missionCompleteText.text = ""; // Löschen Sie auch den Missionsabschlusstext
    }

    void DisplayMissionCompleteText()
    {
        missionCompleteText.text = "Congrats! you have finished all missions";
    }
}
