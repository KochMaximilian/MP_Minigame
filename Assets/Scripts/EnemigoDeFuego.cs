using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoDeFuego : MonoBehaviour
{
    public GameObject bolaFUEGO;
    public float contador;
    public float tiempo;
    public GameObject puntoDisparo;
    public int vidaEnemigo = 6;
    public GameObject jugador;
    public int rango;
    public Animator DISPARO;
    public Animator MUERTE;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        contador = contador + Time.deltaTime;

        detectarPlayer();
        
        if (vidaEnemigo <= 0)
        {
            StartCoroutine("muerte");
            
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fuego")
        {
            vidaEnemigo--;

            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "FuegoCargado")
        {
            vidaEnemigo--;
            vidaEnemigo--;
        }

    }
    void detectarPlayer()
    {
        Vector3 distPlayer = jugador.transform.position - this.transform.position;
        if (distPlayer.magnitude < rango)
        {
            if (contador > tiempo)
            {
                DISPARO.SetBool("ATAQUE", true);
                
                contador = 0;
                StartCoroutine("recarga");
            }
        }
    }
    public IEnumerator recarga()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(bolaFUEGO, puntoDisparo.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        DISPARO.SetBool("ATAQUE", false);
    }
    public IEnumerator muerte()
    {
       
        yield return new WaitForSeconds(0.1f);
        MUERTE.SetBool("MUERTE", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    
}
