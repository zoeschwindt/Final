using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour, IEnemigo
{
    private enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Death
    }

    private Rigidbody2D rig;
    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;

    public int vida = 5; // Vida del enemigo
    public int dañoAlJugador = 1; // Daño al jugador
    public GameObject deathPrefab; // Prefab que aparece cuando muere

    private EnemyState currentState = EnemyState.Idle;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

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

            case EnemyState.Death:
                StartCoroutine(SpawnDeathPrefab());
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

        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;

        }

        if (IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState.Attack);
        }

        if (!IsPlayerInRange(detectionRange))
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private void UpdateAttack()
    {
        if (player == null) return;


        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Ataque");

            // Aplicar daño al jugador si está en rango
            var playerHealth = player.GetComponent<BarraDeVida>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dañoAlJugador); // Llamar al método TakeDamage() del jugador
            }

            lastAttackTime = Time.time;
        }

        if (IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState.Attack);
        }
        else
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

    // Método que recibe daño
    public void TakeDamage(int dañoRecibido)
    {
        vida -= dañoRecibido;

        if (vida <= 0)
        {
            ChangeState(EnemyState.Death);
        }
    }

    private IEnumerator SpawnDeathPrefab()
    {
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Muerte");

        // Espera a que la animación termine, por ejemplo 3 segundos
        yield return new WaitForSeconds(0.6f); // Ajusta este tiempo según lo necesites

        // Instancia el prefab en la posición del enemigo
        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }

        // Destruye el enemigo después de la animación de muerte
        Destroy(gameObject);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }
}






