using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public void returnTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void endCreditQuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
