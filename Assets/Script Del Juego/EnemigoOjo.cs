using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemigoOjo : MonoBehaviour, IEnemigo
{
    public Transform player;                
    public float detectionRange = 5f;        
    public float attackRange = 1f;           
    public float moveSpeed = 2f;             

    public int currentHealth = 3;           
    private bool isDead = false;             
    private Animator animator; 
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private GameObject deathPrefab;
    public Contador contador;

    public int damage = 1;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return; 

        if (Vector3.Distance(transform.position, player.position) <= attackRange && !isDead)
        {
            AttackPlayer();
        }

        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isDead)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        spriteRenderer.flipX = player.position.x > transform.position.x ? false : true;
    }

    
    private void AttackPlayer()
    {
        animator.SetTrigger("Ataque"); 
        
    }

    
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage; 
            Debug.Log($"Vida del enemigo: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die(); 
            }
        }
    }

    
    private void Die()
    {
        isDead = true; 
        animator.SetTrigger("Muerte"); 
        
        Collider2D collider2D = gameObject.GetComponent<Collider2D>();
        collider2D.enabled = false;
        
        StartCoroutine(SpawnDeathPrefab());
    }

    
    private IEnumerator SpawnDeathPrefab()
    {
        
        yield return new WaitForSeconds(3f); 

        Instantiate(deathPrefab, transform.position, Quaternion.identity);

      
        Destroy(gameObject, 2f); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<BarraDeVida>().TakeDamage(damage);

        }
    }
    private void OnDestroy()
    {
        
        if (contador != null)
        {
            contador.EnemyDestroyed(gameObject);
        }



    }
}
