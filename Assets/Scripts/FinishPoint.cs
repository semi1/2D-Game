using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public GameObject levelPanel; // Assign your level panel

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowLevelMenu();
            //SceneManager.LoadScene("Main Menu");
        }
    }


    void ShowLevelMenu()
    {
        levelPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
