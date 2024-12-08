using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemigoOjo : MonoBehaviour
{
    public Transform player;                // Referencia al jugador
    public float detectionRange = 5f;        // Rango de detecci�n
    public float attackRange = 1f;           // Rango de ataque
    public float moveSpeed = 2f;             // Velocidad de movimiento

    private int currentHealth = 3;           // Salud del enemigo
    private bool isDead = false;             // Verifica si el enemigo est� muerto
    [SerializeField] private Animator animator; // Referencia al Animator

    // Prefab que aparecer� despu�s de la muerte
    [SerializeField] private GameObject deathPrefab;

    void Update()
    {
        if (isDead) return; // Si el enemigo est� muerto, no hace nada

        // Si el jugador est� dentro del rango de ataque, el enemigo ataca
        if (Vector3.Distance(transform.position, player.position) <= attackRange && !isDead)
        {
            AttackPlayer();
        }

        // Si el jugador entra en el rango de detecci�n, persigue al jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isDead)
        {
            ChasePlayer();
        }
    }

    // Persigue al jugador
    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // L�gica de ataque (puedes implementar lo que quieras aqu�)
    private void AttackPlayer()
    {
        animator.SetTrigger("Ataque"); // Reproduce la animaci�n de ataque
        Debug.Log("�El enemigo ataca al jugador!");
    }

    // M�todo para que el enemigo reciba da�o de las balas
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage; // Reduce la vida del enemigo
            Debug.Log($"Vida del enemigo: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die(); // Llama al m�todo de muerte si la vida llega a cero
            }
        }
    }

    // L�gica de muerte
    private void Die()
    {
        isDead = true; // Marca al enemigo como muerto
        animator.SetTrigger("Muerte"); // Activa la animaci�n de muerte
        Debug.Log("�El enemigo ha muerto!");

        // Despu�s de 3 segundos de la animaci�n de muerte, aparece el objeto
        StartCoroutine(SpawnDeathPrefab());
    }

    // M�todo que se ejecuta despu�s de la animaci�n de muerte
    private IEnumerator SpawnDeathPrefab()
    {
        // Espera a que la animaci�n termine, por ejemplo 3 segundos
        yield return new WaitForSeconds(3f); // Ajusta este tiempo seg�n lo necesites

        // Instancia el prefab en la posici�n del enemigo
        Instantiate(deathPrefab, transform.position, Quaternion.identity);

        // Destruye el enemigo despu�s de la animaci�n de muerte
        Destroy(gameObject, 2f); // Destruye el enemigo tras 2 segundos para que se vea la animaci�n
    }

    // M�todo para detectar colisi�n con la bala
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            TakeDamage(1); // Llama a la funci�n para reducir la vida del enemigo
            Destroy(collision.gameObject); // Destruye la bala al colisionar
        }
    }
}
