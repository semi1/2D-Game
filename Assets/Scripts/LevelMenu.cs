using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
   
    public void OpenLevel(int levelId)
    {

        Time.timeScale = 1f;
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

}
