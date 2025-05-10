using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


    [SerializeField] GameObject gameOver;
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
