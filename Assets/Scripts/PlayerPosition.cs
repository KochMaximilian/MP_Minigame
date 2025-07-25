using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    public Checkpoint cp;
    public AtaqueDistancia jugador;
    
    public Animator MUERTE;
    public bool UNO;
    public GameObject PLAYER;
    public Movimiento mov;
    public SaltoDoble salto;
    public AudioSource muertesonido;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
         UNO = false;
    cp = GameObject.FindGameObjectWithTag("CP").GetComponent<Checkpoint>();
        transform.position = cp.posicionCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
       if(jugador.MUERTO == true && UNO==false )
        {
           
            MUERTE.SetBool("MUERTE", true);
            
            StartCoroutine("reaparece");
            UNO = true;
            mov.mover = false;
            

        }
    }

    public IEnumerator reaparece()
    {
        yield return new WaitForSeconds(3f);
        transform.position = cp.posicionCheckpoint;
        
        jugador.MUERTO = false;
        jugador.vida = 1000;
        MUERTE.SetBool("MUERTE", false);
        UNO = false;
        mov.mover = true;
        salto.vel = 13;
        yield return new WaitForSeconds(0.1f);
        muertesonido.Play();


    }
    public IEnumerator muerte()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = cp.posicionCheckpoint;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Muerte")
        {
            transform.position = cp.posicionCheckpoint;

            
        }
    }

}
