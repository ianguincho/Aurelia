using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    //public GameObject completeLevelUI;
    public float restartDelay = 2f;
    //public void CompleteLevel()
    //{
    //    completeLevelUI.SetActive(true);
    //}
    public void EndGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("Game over.");
            Invoke("Restart", restartDelay);
        }

    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

}
