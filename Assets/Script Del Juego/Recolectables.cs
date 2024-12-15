using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Recolectables : MonoBehaviour
{
    public TextMeshProUGUI puntosTexto;
    private int puntos = 0;

    public BarraDeVida barraDeVida; 
    public Tiempoo tiempoController; 

    private void Start()
    {
        ActualizarPuntos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Moneda"))
        {
            puntos++;  
            ActualizarPuntos();
            Destroy(collision.gameObject);  
        }
        else if (collision.CompareTag("Vida"))
        {
            barraDeVida.AddLife(1); 
            Destroy(collision.gameObject);  
        }
        else if (collision.CompareTag("Time"))
        {
            tiempoController.AddTime(10); 
            Destroy(collision.gameObject); 
        }
        else if (collision.CompareTag("Monedass"))
        {
            puntos += 4;  
            ActualizarPuntos();  
            Destroy(collision.gameObject);  
        }
    }

    private void ActualizarPuntos()
    {
        puntosTexto.text = puntos.ToString();  
    }



}
