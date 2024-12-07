using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Recolectables : MonoBehaviour
{
    public TextMeshProUGUI puntosTexto;
    private int puntos = 0;

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
    }

    private void ActualizarPuntos()
    {
        puntosTexto.text = puntos.ToString();
    }

}
