using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float velocidadPersonaje;
    [SerializeField] private float fuerzaSalto;

    private bool enSuelo;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float velocidadInput = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(velocidadInput * velocidadPersonaje, rig.velocity.y);

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rig.velocity = new Vector2(rig.velocity.x, fuerzaSalto);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            enSuelo = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            enSuelo = false;
        }
    }

}
