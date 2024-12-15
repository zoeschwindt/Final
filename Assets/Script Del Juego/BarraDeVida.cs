using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BarraDeVida : MonoBehaviour
{

    public int maximaVida = 5; 
    private int vidaActual;

  
    public GameObject botellaPrefab; 
    public Transform healthUIParent;   

    public Pantalla pantalla;

    private List<GameObject> rumBottles = new List<GameObject>();

    void Start()
    {
        vidaActual = maximaVida; 
        InicializarVidaUI();      
    }

    void InicializarVidaUI()
    {
        
        for (int i = 0; i < maximaVida; i++)
        {
            GameObject bottle = Instantiate(botellaPrefab, healthUIParent);
            bottle.transform.localScale = Vector3.one; 
            rumBottles.Add(bottle);
        }
    }

    
    public void TakeDamage(int damage)
    {
        if (vidaActual > 0)
        {
            vidaActual -= damage; 
            UpdateHealthUI();         
        }

        if (vidaActual <= 0)
        {
            Die(); 
        }
    }

    void UpdateHealthUI()
    {
        
        for (int i = 0; i < rumBottles.Count; i++)
        {
            rumBottles[i].SetActive(i < vidaActual);
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
       pantalla.ShowDefeatScreen(); 
    }

    public void AddLife(int amount)
    {
        if (vidaActual < maximaVida)
        {
            vidaActual += amount; 
            UpdateHealthUI();        
        }
    }

}