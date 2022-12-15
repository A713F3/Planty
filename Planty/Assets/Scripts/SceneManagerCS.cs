using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCS : MonoBehaviour
{
    private string currentSceneName;
    private int levelNum;

    
    void Start()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        levelNum = SceneManager.GetActiveScene().buildIndex;
    }

    public void nextLevel()
    {

        if(currentSceneName == "Level5"){
            MainMenu();
        }
        else{
            SceneManager.LoadScene(levelNum + 1);
        }

        
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
