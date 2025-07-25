using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SONIDO : MonoBehaviour
{
    public GameObject Sonidotemplo;
    public GameObject Sonidojungla;
    public Collider Desactivado;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioSource sonidos = Sonidojungla.GetComponent<AudioSource>();
            sonidos.Pause();
            AudioSource sonidos2 = Sonidotemplo.GetComponent<AudioSource>();
            sonidos2.Play();
            Destroy(Desactivado);
        }
    }
}
