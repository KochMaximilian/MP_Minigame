using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    public Camera camara;
    public Camera camara2;
    
    public Animator aniPuerta;
    public Animator aniPuerta2;
    public bool activado=false;
    public AudioSource sonidoPuerta;
    public GameObject totem1;
    public Animator habilidad;
    public Animator camaraani;
    public GameObject PETALOS;
    public GameObject puertaactiva;
    
    // Start is called before the first frame update
    void Start()
    {
        camara.enabled = true;
        camara2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        puertaactiva.SetActive(true);
        if(other.tag == "Player" && activado==false)
        {
            StartCoroutine("Camara");
            activado = true;
            totem1.SetActive(true);
            
            PETALOS.GetComponent<SphereCollider>();
            SphereCollider peta = PETALOS.GetComponent<SphereCollider>();
            peta.isTrigger = true;
        }
      
    }

    IEnumerator Camara()
    {
        yield return new WaitForSeconds(1f);
        sonidoPuerta.Play();
        camaraani.SetBool("Puerta", true);
        camara.enabled = false;
            camara2.enabled = true;
        habilidad.SetBool("PETALOS", true);
        yield return new WaitForSeconds(1f);
        
        aniPuerta.SetBool("Puerta",true);
        aniPuerta2.SetBool("Puerta", true);
        yield return new WaitForSeconds(3f);
        camara.enabled = true;
        camara2.enabled = false;
       

    }
}
