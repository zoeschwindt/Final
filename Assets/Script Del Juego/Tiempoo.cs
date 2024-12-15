using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempoo : MonoBehaviour
{
    public TMP_Text timerText;
    private float timeRemaining = 25f; 
    public Pantalla pantalla;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = (int)(timeRemaining / 60);
            int seconds = (int)(timeRemaining % 60);
            timerText.text = minutes + ":" + seconds.ToString("00"); 
        }
        else
        {
            timerText.text = "0:00"; 
            Debug.Log("¡Se acabó el tiempo!");
            pantalla.ShowDefeatScreen(); 
        }
    }

  
    public void AddTime(float extraTime)
    {
        timeRemaining += extraTime; 
        if (timeRemaining < 0) 
        {
            timeRemaining = 0;
        }
    }

}
