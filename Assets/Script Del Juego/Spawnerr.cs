using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerr : MonoBehaviour
{
    [SerializeField] private GameObject balaPrefab; 
    [SerializeField] private Transform puntoDisparo; 
    [SerializeField] private float fuerzaDisparo = 10f; 

    private bool mirandoDerecha = true;


    private void Girar() 
    {

        mirandoDerecha = !mirandoDerecha;

    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            Disparar();
        }

        float velocidadInput = Input.GetAxisRaw("Horizontal");

        if (velocidadInput > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (velocidadInput < 0 && mirandoDerecha)
        {
            Girar();
        }

    }

    private void Disparar()
    {
        
        GameObject balaInstancia = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);

       
        Rigidbody2D rigidbodyBala = balaInstancia.GetComponent<Rigidbody2D>();
        if (rigidbodyBala != null)
        {


            if (mirandoDerecha)
            {
                rigidbodyBala.velocity = puntoDisparo.right * fuerzaDisparo; 
            }
            else 
            {
                rigidbodyBala.velocity = - puntoDisparo.right * fuerzaDisparo; 
            }
        }


       
        Destroy(balaInstancia, 1f);

        
    }
} 

