using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    public Pantalla pantalla;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        // Verificar si el objeto tiene el tag Victory
        if (collision.CompareTag("Player"))
        {
          pantalla.ShowVictoryScreen();
        }
    }
}
