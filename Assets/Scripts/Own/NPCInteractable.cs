using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public GameObject canvasObject; // Reference to your Canvas GameObject

    public void Interact()
    {
        Debug.Log("Interact!");


        // Check if the canvasObject is not null before trying to enable it
        if (canvasObject != null)
        {
            // Enable the Canvas GameObject
            canvasObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas GameObject is not assigned to the script!");
        }

    }
}