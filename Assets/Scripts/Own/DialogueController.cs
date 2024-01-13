using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public InputActionProperty ActionButton;
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    public float DialogueSpeed;

    private int Index;


    // Start is called before the first frame update
    void Start()
    {
        DialogueText.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if (ActionButton.action.WasPerformedThisFrame())
        {
            if (DialogueText.text == Sentences[Index])
            {
                NextSentence();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = Sentences[Index];
            }
        }
    }
    void StartDialogue()
    {
        Index = 0;
        StartCoroutine(WriteSentence());
    }
    void NextSentence()
    {
        if (Index < Sentences.Length - 1)
        {
            Index++;
            DialogueText.text = string.Empty;
            StartCoroutine(WriteSentence());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator WriteSentence()
    {
        foreach (char Character in Sentences[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
    }

}