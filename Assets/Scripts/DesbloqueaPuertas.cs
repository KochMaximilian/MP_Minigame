using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloqueaPuertas : MonoBehaviour
{
    public int estatuas1 = 0;
    public int estatuas2 = 0;

    public Animator puerta1;
    public Animator puerta1der;
    public Animator puertaFinal;
    public Animator puertaFinal2;

    public Camera camara;
    public Camera camara2;
    public Camera camara3;

    public bool desactivado1 = false;
    public bool desactivado2 = false;
    public bool desactivado3 = false;
    public bool desactivado4 = false;
    public bool desactivado5 = false;
    public bool desactivado6 = false;
    public bool desactivado7 = false;

    public GameObject textoPuertaTemplo;
    public GameObject textoPuertaFinal;

    public GameObject totem1;
    public GameObject totem2;
    public GameObject totem3;
    public GameObject totem4;
    public GameObject totem5;
    public GameObject totem6;
    public GameObject totem7;

    public GameObject puerta2activada;
    public GameObject puerta3activada;

    public AudioSource sonidoTemplo;
    public AudioSource sonidoFinal;


    // Start is called before the first frame update
    void Start()
    {
        camara.enabled = true;
        camara2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estatuas1 == 4)
        {
            puerta2activada.SetActive(true);
            estatuas1 = 0;
            StartCoroutine("Camara");
            
           

        }
        if (estatuas2 == 3)
        {
            puerta3activada.SetActive(true);
            estatuas2 = 0;
            StartCoroutine("Camara2");
            

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Estatua1" && desactivado1 ==false )
        {
            estatuas1++;
            desactivado1 = true;
            totem1.SetActive(true);
        }

        if (other.gameObject.tag == "Estatua2" && desactivado2 == false)
        {
            estatuas1++;
            desactivado2 = true;
            totem2.SetActive(true);
        }
        if (other.gameObject.tag == "Estatua3" && desactivado3 == false)
        {
            estatuas1++;
            desactivado3 = true;
            totem3.SetActive(true);
        }
        if (other.gameObject.tag == "Estatua4" && desactivado4 == false)
        {
            estatuas1++;
            desactivado4 = true;
            totem4.SetActive(true);
        }

        if (other.gameObject.tag == "Estatua5" && desactivado5 == false)
        {
            estatuas2++;
            desactivado5 = true;
            totem5.SetActive(true);
        }
        if (other.gameObject.tag == "Estatua6" && desactivado6 == false)
        {
            estatuas2++;
            desactivado6 = true;
            totem6.SetActive(true);
        }
        if (other.gameObject.tag == "Estatua7" && desactivado7 == false)
        {
            estatuas2++;
            desactivado7 = true;
            totem7.SetActive(true);
        }








    }
    IEnumerator Camara()
    {
        yield return new WaitForSeconds(2f);
        camara.enabled = false;
        camara2.enabled = true;
        sonidoTemplo.Play();
        yield return new WaitForSeconds(1f);

        puerta1.SetBool("Abierta", true);
        puerta1der.SetBool("Abierta", true);
       

    yield return new WaitForSeconds(3f);
        camara.enabled = true;
        camara2.enabled = false;
      
        
      

    }
    IEnumerator Camara2()
    {
        yield return new WaitForSeconds(2f);
        camara.enabled = false;
        camara3.enabled = true;
        sonidoFinal.Play();
        yield return new WaitForSeconds(1f);

        puertaFinal.SetBool("Completada", true);
        puertaFinal2.SetBool("Completada", true);

        yield return new WaitForSeconds(3f);
        camara.enabled = true;
        camara3.enabled = false;

       
    }
}
