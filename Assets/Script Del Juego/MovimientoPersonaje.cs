using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{

        private Rigidbody2D rig;
        [SerializeField] private float velocidadPersonaje;
        [SerializeField] private float fuerzaSalto;
        [SerializeField] private Animator animator;

        private bool enSuelo;
        private bool mirandoDerecha = true;


        private enum Estado
        {
            Idle,
            Walk,
            Jump,
            Shoot
        }
        private Estado estadoActual = Estado.Idle;

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float velocidadInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && enSuelo)
            {
                CambiarEstado(Estado.Jump);
                rig.velocity = new Vector2(rig.velocity.x, fuerzaSalto);
                enSuelo = false;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                CambiarEstado(Estado.Shoot);
            }

            if (Mathf.Abs(velocidadInput) > 0.1f)
            {
                CambiarEstado(Estado.Walk);

            }
            else
            {
                CambiarEstado(Estado.Idle);
            }



            rig.velocity = new Vector2(velocidadInput * velocidadPersonaje, rig.velocity.y);


            if (velocidadInput > 0 && !mirandoDerecha)
            {
                Girar();
            }
            else if (velocidadInput < 0 && mirandoDerecha)
            {
                Girar();
            }
        }

        private void CambiarEstado(Estado nuevoEstado)
        {
            if (estadoActual == nuevoEstado) return;

            estadoActual = nuevoEstado;

            switch (estadoActual)
            {
                case Estado.Idle:
                    animator.SetBool("Caminar", false);
                    break;

                case Estado.Walk:
                    animator.SetBool("Caminar", true);
                    break;
                case Estado.Jump:
                    animator.SetBool("Salto", true);
                    break;
                case Estado.Shoot:
                    animator.SetTrigger("Disparo");

                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Piso"))
            {
                enSuelo = true;
                animator.SetBool("Salto", false);
            }
        }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        float playerEscala;

        if (mirandoDerecha)
        {
            playerEscala = Mathf.Abs(transform.localScale.x);
        }
        else
        {
            playerEscala = -Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = new Vector3(playerEscala, transform.localScale.y, transform.localScale.z);
    }




}


