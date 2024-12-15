using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BarraDeVida : MonoBehaviour
{

    public int maximaVida = 5; // Máxima vida del jugador
    private int vidaActual;

  
    public GameObject botellaPrefab; // Prefab de las botellas
    public Transform healthUIParent;   // Contenedor en el Canvas para las botellas

    public Pantalla pantalla;

    private List<GameObject> rumBottles = new List<GameObject>(); // Lista de las botellas instanciadas

    void Start()
    {
        vidaActual = maximaVida; // Inicializa la vida al máximo
        InicializarVidaUI();      // Crea las botellas de vida en la UI
    }

    void InicializarVidaUI()
    {
        // Instancia las botellas y las posiciona en el contenedor
        for (int i = 0; i < maximaVida; i++)
        {
            GameObject bottle = Instantiate(botellaPrefab, healthUIParent);
            bottle.transform.localScale = Vector3.one; // Asegura la escala correcta
            rumBottles.Add(bottle);
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        if (vidaActual > 0)
        {
            vidaActual -= damage; // Resta la vida en función del daño recibido
            UpdateHealthUI();         // Actualiza la UI de vida
        }

        if (vidaActual <= 0)
        {
            Die(); // Llama a la función de muerte si la vida llega a cero
        }
    }

    void UpdateHealthUI()
    {
        // Actualiza las botellas visibles según la vida actual
        for (int i = 0; i < rumBottles.Count; i++)
        {
            rumBottles[i].SetActive(i < vidaActual);
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
       pantalla.ShowDefeatScreen(); // Muestra la pantalla de derrota
    }

    public void AddLife(int amount)
    {
        if (vidaActual < maximaVida)
        {
            vidaActual += amount; // Suma vida
            UpdateHealthUI();        // Actualiza la UI de vida
        }
    }

}