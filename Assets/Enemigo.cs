using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    private Rigidbody2D rig;
    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;

    public int vida = 5; // Vida del enemigo
    public int da�oAlJugador = 1; // Da�o al jugador
    public GameObject deathPrefab; // Prefab que aparece cuando muere

    private EnemyState currentState = EnemyState.Idle;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Chase:
                UpdateChase();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdateIdle()
    {
        animator.SetBool("Caminar", false);

        if (IsPlayerInRange(detectionRange))
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void UpdateChase()
    {
        animator.SetBool("Caminar", true);

        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rig.velocity = new Vector2(direction.x * moveSpeed, rig.velocity.y);

        if (IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState.Attack);
        }
    }

    private void UpdateAttack()
    {
        if (player == null) return;

        rig.velocity = Vector2.zero;
        animator.SetTrigger("Ataque");

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Aplicar da�o al jugador si est� en rango
            var playerHealth = player.GetComponent<BarraDeVida>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(da�oAlJugador); // Llamar al m�todo TakeDamage() del jugador
            }

            lastAttackTime = Time.time;
        }

        if (!IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.position) <= range;
    }

    // M�todo que recibe da�o
    public void TakeDamage(int da�oRecibido)
    {
        vida -= da�oRecibido;

        if (vida <= 0)
        {
            Die();
        }
    }

    // Animaci�n y destrucci�n cuando el enemigo muere
    private void Die()
    {
        animator.SetTrigger("Muerte");

        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 0.5f); // Destruye al enemigo despu�s de la animaci�n
    }

    // Detecta balas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            TakeDamage(1); // El enemigo recibe 1 de da�o
            Destroy(collision.gameObject); // Destruye la bala
        }
    }
}




