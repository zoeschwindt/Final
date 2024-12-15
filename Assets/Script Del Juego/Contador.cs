using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public TMP_Text scoreText; // Campo para el texto TMP en el Canvas
    private int score = 0; // Puntaje inicial

    // Método que suma 1 punto al contador
    public void AddPoint()
    {
        score++; // Incrementa el puntaje
       scoreText.text = score.ToString(); 
    }

    // Método que se llama cuando un enemigo es destruido
    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemy.CompareTag("Enemigo")) // Verifica si el enemigo tiene la etiqueta "Enemigo"
        {
            AddPoint(); // Suma un punto
            Destroy(enemy); // Destruye el enemigo
        }
    }
}


