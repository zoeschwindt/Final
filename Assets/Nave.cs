using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Nave : MonoBehaviour
{
    [SerializeField] float movSpeed;
    private float playerMov;

    private int fuerzaDeSalto = 5;
    private bool estaSaltando = false;

    void Update()
    {
        
    }
    void Movimiento() 
    {
        playerMov = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(playerMov * movSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && estaSaltando == false)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, fuerzaDeSalto);
            estaSaltando = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "piso")
        {
            estaSaltando = false;
        }
    }
}
