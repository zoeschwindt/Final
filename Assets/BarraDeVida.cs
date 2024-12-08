using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BarraDeVida : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5; // Máxima vida del jugador
    private int currentHealth;

    [Header("UI Settings")]
    public GameObject rumBottlePrefab; // Prefab de las botellas
    public Transform healthUIParent;   // Contenedor en el Canvas para las botellas

    private List<GameObject> rumBottles = new List<GameObject>(); // Lista de las botellas instanciadas

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida al máximo
        InitializeHealthUI();      // Crea las botellas de vida en la UI
    }

    void InitializeHealthUI()
    {
        // Instancia las botellas y las posiciona en el contenedor
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject bottle = Instantiate(rumBottlePrefab, healthUIParent);
            bottle.transform.localScale = Vector3.one; // Asegura la escala correcta
            rumBottles.Add(bottle);
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage; // Resta la vida en función del daño recibido
            UpdateHealthUI();         // Actualiza la UI de vida
        }

        if (currentHealth <= 0)
        {
            Die(); // Llama a la función de muerte si la vida llega a cero
        }
    }

    void UpdateHealthUI()
    {
        // Actualiza las botellas visibles según la vida actual
        for (int i = 0; i < rumBottles.Count; i++)
        {
            rumBottles[i].SetActive(i < currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Aquí puedes añadir más lógica, como reiniciar el nivel o mostrar una pantalla de "Game Over"
    }

    // Detecta si el jugador colisiona con un enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            TakeDamage(1); // Llama a la función para reducir la vida del jugador
        }
    }
}