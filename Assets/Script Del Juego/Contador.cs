using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public TMP_Text scoreText; 
    private int score = 0;

    public void AddPoint()
    {
        score++; 
       scoreText.text = score.ToString(); 
    }

   
    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemy.CompareTag("Enemigo")) 
        {
            AddPoint(); 
            Destroy(enemy); 
        }
    }
}


