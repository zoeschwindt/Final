using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Recolectables : MonoBehaviour
{
    public TextMeshProUGUI puntosTexto;
    private int puntos = 0;

    public BarraDeVida barraDeVida; // Referencia al script de la barra de vida
    public Tiempoo tiempoController; // Referencia al script que maneja el tiempo

    private void Start()
    {
        ActualizarPuntos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moneda"))
        {
            puntos++;  // Se suman 1 punto por cada moneda normal
            ActualizarPuntos();
            Destroy(collision.gameObject);  // Destruye la moneda
        }
        else if (collision.CompareTag("Vida"))
        {
            barraDeVida.AddLife(1); // Suma una vida al jugador
            Destroy(collision.gameObject);  // Destruye el prefab de vida
        }
        else if (collision.CompareTag("Time"))
        {
            tiempoController.AddTime(10); // Suma 10 segundos al tiempo
            Destroy(collision.gameObject); // Destruye el prefab de tiempo
        }
        else if (collision.CompareTag("Monedass"))
        {
            puntos += 4;  // Se suman 4 puntos por el prefab de monedas del enemigo
            ActualizarPuntos();  // Actualiza el texto de los puntos
            Destroy(collision.gameObject);  // Destruye el prefab de monedas
        }
    }

    private void ActualizarPuntos()
    {
        puntosTexto.text = puntos.ToString();  // Actualiza el texto de puntos
    }



}
