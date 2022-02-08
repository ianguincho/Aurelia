using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    bool gameEnded = false;
    //public GameObject completeLevelUI;
    public float restartDelay = 2f;
    //public void CompleteLevel()
    //{
    //    completeLevelUI.SetActive(true);
    //}

    //Singleton instantiation
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public void EndGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("Game over.");
            Invoke("Restart", restartDelay);
        }

    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

}
