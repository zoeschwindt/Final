using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pantalla : MonoBehaviour
{
    public GameObject gameover; 
    public TMP_Text textgameover;
    public AudioSource audioSource;
    public AudioClip audioVictory;
    public AudioClip audioGameover;
    public AudioClip audioGameplay;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioGameplay;
        audioSource.Play();
        
        if (gameover != null)
        {
            gameover.SetActive(false);
        }
        
    }
    

   
    public void ShowVictoryScreen()
    {
        if (gameover != null)
        {
            audioSource.clip = audioVictory;
            audioSource.Play();
            gameover.SetActive(true); 
            textgameover.text = "YOU WIN!";
            Time.timeScale = 0; 
        }
        
    }

    public void ShowDefeatScreen()
    {
        if (gameover != null)
        {
            audioSource.clip = audioGameover;
            audioSource.Play();
            gameover.SetActive(true); 
            textgameover.text = "GAME OVER";
            Time.timeScale = 0; 
        }
        
    }

}


