using UnityEngine;
using TMPro;

public class LevelBehaviour : MonoBehaviour
{
    public TMP_Text levelText;
    public int levelNumber = 1;

    void Start()
    {
        levelText.text = "Level " + levelNumber;
        levelNumber++;
    }
}
