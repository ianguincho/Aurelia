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
    
    public void Load()
    {
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            string levelToLoad = PlayerPrefs.GetString("LevelSaved");
            SceneManager.LoadScene(levelToLoad);
        }
    }
    public void openOptions()
    {
        optionsScreen.SetActive(true);
        snowParticleGenerator.SetActive(false);
    }

    public void closeOptions()
    {
        optionsScreen.SetActive(false);
        snowParticleGenerator.SetActive(true);
    }

    public void openCredits()
    {
        creditsScreen.SetActive(true);
        snowParticleGenerator.SetActive(false);
    }

    public void closeCredits()
    {
        creditsScreen.SetActive(false);
        snowParticleGenerator.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
        //code for debug purposes
        Debug.Log("Exit");
    }
}
