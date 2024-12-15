using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempoo : MonoBehaviour
{
    public TMP_Text timerText;
    private float timeRemaining = 25f; // 2 minutos en segundos
    public Pantalla pantalla;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = (int)(timeRemaining / 60);
            int seconds = (int)(timeRemaining % 60);
            timerText.text = minutes + ":" + seconds.ToString("00"); // Muestra MM:SS
        }
        else
        {
            timerText.text = "0:00"; // Tiempo terminado
            Debug.Log("¡Se acabó el tiempo!");
            pantalla.ShowDefeatScreen(); // Muestra la pantalla de derrota
        }
    }

    // Método para añadir tiempo al temporizador
    public void AddTime(float extraTime)
    {
        timeRemaining += extraTime; // Suma los segundos al tiempo restante
        if (timeRemaining < 0) // Evita tiempos negativos
        {
            timeRemaining = 0;
        }
    }

}
