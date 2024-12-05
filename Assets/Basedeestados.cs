using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Basedeestados : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;

    private EnemyState currentState = EnemyState.Idle;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

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
        Debug.Log("Estado: Idle");
        if (IsPlayerInRange(detectionRange))
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void UpdateChase()
    {
        Debug.Log("Estado: Chase");
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;


        if (IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState. Attack);
        }
        else if (!IsPlayerInRange(detectionRange))
        {
            
        }
    }
    private void UpdateAttack()
    {
        Debug.Log("Estado: Attack");

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        if (!IsPlayerInRange(attackRange))
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        Debug.Log($"Cambiando de estado:  {currentState} ->  {newState}");
        currentState = newState;
    }
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.position) <= range;
    }

    private void AttackPlayer()
    {
        Debug.Log("¡El enemigo ataca al jugador");
    }
}
