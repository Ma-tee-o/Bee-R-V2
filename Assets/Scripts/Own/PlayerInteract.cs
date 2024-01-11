using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public InputActionProperty ActionButton;
    public float InteractRange = 2f;


    // Update is called once per frame
    void Update()
    {
        if (ActionButton.action.WasPerformedThisFrame())
        {
            Collider [] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
            foreach (Collider collider in colliderArray) 
            {
               if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    Debug.Log("Interacting with: " + collider.gameObject.name);
                    npcInteractable.Interact();
                }
               else
                {
                    Debug.LogWarning("No NPCInteractable component found on: " + collider.gameObject.name);
                }
            }
        }
       
    }
    public NPCInteractable GetInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, InteractRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable)) 
            {
                return npcInteractable;
            }
        }
        return null; 
    }
}
