using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BarraDeVida : MonoBehaviour
{
    public int vidaActual;
    public int vidaMaxima;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int cantidadDaño)
    {
        int vidaTemporal = vidaActual - cantidadDaño;

        if (vidaTemporal < 0)
        {
            vidaActual = 0;
        }
        else
        {
            vidaActual = vidaTemporal;
        }
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CurarVida(int cantidadCuracion)
    {
        int vidaTemporal = vidaActual + cantidadCuracion;
        if (vidaTemporal > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
        else
        {
            vidaActual = vidaTemporal;
        }
    }
}
