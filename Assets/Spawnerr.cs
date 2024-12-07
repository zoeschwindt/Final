using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerr : MonoBehaviour
{
    [SerializeField] private GameObject balaPrefab; // Prefab del proyectil
    [SerializeField] private Transform puntoDisparo; // Lugar desde donde se dispara
    [SerializeField] private float fuerzaDisparo = 10f; // Velocidad del proyectil

    private bool mirandoDerecha = true;

    private void Girar()
    {
        float velocidadInput = Input.GetAxisRaw("Horizontal");

        if (velocidadInput > 0 && !mirandoDerecha)
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (velocidadInput < 0 && mirandoDerecha)
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        // Verificar si el jugador no está muerto (puedes adaptarlo a tu lógica)
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        {
            Disparar();
        }
        Girar();
        
    }

    private void Disparar()
    {
        // Instanciar la bala en el punto de disparo
        GameObject balaInstancia = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);

        // Obtener el Rigidbody2D de la bala y aplicar la fuerza
        Rigidbody2D rigidbodyBala = balaInstancia.GetComponent<Rigidbody2D>();
        if (rigidbodyBala != null)
        {
            rigidbodyBala.velocity = puntoDisparo.right * fuerzaDisparo; // En 2D, se usa 'right' para la dirección horizontal
        }

        // Destruir la bala después de un tiempo
        Destroy(balaInstancia, 1f);
    }
} 
