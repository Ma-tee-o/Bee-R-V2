using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public void QuitGame_()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    } 
}

