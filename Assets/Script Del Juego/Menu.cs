using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Play()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menuu()
    {
        Time.timeScale = 1.0f; 
        SceneManager.LoadScene(0);
    }
}
