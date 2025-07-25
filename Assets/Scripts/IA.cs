using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    
    public NavMeshAgent miAgente;
    public float margen = 1;
    public int destinoActual;
    public GameObject punto1;
    public GameObject punto2;
    public GameObject jugador;
    public Collider arañaIA;
    public float rango=10;
    public float anguloVista=45;

    public int vidaAraña = 5;

    public Animator ATAQUEARAÑA;
    public NavMeshAgent araña;

    public AtaqueDistancia script;
    // Start is called before the first frame update
    void Start()
    {
        ATAQUEARAÑA.SetBool("Camina", true);
        patrulla();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        patrulla();
        detectarPlayer();
        if(vidaAraña <=0)
        {
            ATAQUEARAÑA.SetBool("Muere", true);
            araña.speed = 0;
            arañaIA = GetComponent<Collider>();
            arañaIA.isTrigger = true;
            
            Destroy(gameObject, 5f);
        }
        
    }


   
    public void patrulla()
    {



        Vector3 dist = this.transform.position - miAgente.destination;
        if (dist.magnitude < margen)
        {
            if (destinoActual == 1)
            {
                destinoActual = 2;
                miAgente.SetDestination(punto1.transform.position);
            }
            else
            {
                destinoActual = 1;
                miAgente.SetDestination(punto2.transform.position);
            }
        }
    }
    void detectarPlayer()
    {
        Vector3 distPlayer = jugador.transform.position - this.transform.position;
        if (distPlayer.magnitude < rango)
        {

            RaycastHit resultadoRay;
            if (Physics.Raycast(this.transform.position, distPlayer, out resultadoRay, 20))
            {
                if (resultadoRay.transform.tag == "Player")
                {

                    float angulo = Vector3.Angle(this.transform.forward, distPlayer);

                    if (angulo < anguloVista)
                    {

                        miAgente.SetDestination(jugador.transform.position);

                        
                    }
                    
                   
                }
                


            }
            

        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fuego")
        {
            vidaAraña--;
            if(script.fuerza ==2)
            {
                vidaAraña--;
                
            }


        }
        if(other.gameObject.tag == "FuegoCargado")
        {
            vidaAraña--;
            vidaAraña--;
        }

    }
}
