using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject options;
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
        options.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void openOptions()
    {
        options.SetActive(true);
    }

    public void closeOptions()
    {
        options.SetActive(false);
    }

    public void quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
