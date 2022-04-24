using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    public bool paused;

    [Header("Singleton Instantiation")]
    private static PauseMenu instance;
    public static PauseMenu Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<PauseMenu>();
            return instance;
        }
    }


    private void Start()
    {
        pauseMenu.SetActive(false);
        
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            if (paused)
                closePauseScreen();
            else
                openPauseScreen();
        }
    }

    void openPauseScreen()
    {
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }

    void closePauseScreen()
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
