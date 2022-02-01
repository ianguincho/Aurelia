using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject optionsScreen;
    public GameObject creditsScreen;
    public GameObject snowParticleGenerator;
    
    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void openOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void closeOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void openCredits()
    {
        creditsScreen.SetActive(true);
    }

    public void closeCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void openSnowParticles()
    {
        snowParticleGenerator.SetActive(true);
    }

    public void closeSnowParticles()
    {
        snowParticleGenerator.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
        //code for debug purposes
        Debug.Log("Exit");
    }
}
