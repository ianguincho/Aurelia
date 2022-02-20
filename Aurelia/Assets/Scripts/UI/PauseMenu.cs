using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool paused;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (paused)
        //        closePauseScreen();
        //    else
        //        openPauseScreen();
        //}
    }

    void openPauseScreen()
    {
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }

    public void closePauseScreen()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    

    public void quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
