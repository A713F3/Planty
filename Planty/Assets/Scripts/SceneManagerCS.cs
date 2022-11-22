using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCS : MonoBehaviour
{
    private string currentSceneName;
    
    void Start()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LevelsMenu()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
