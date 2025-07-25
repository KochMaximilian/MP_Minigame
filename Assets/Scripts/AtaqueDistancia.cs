using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtaqueDistancia : MonoBehaviour
{
    public GameObject balaOriginal;
    public GameObject balaCargada;
    
    
    public GameObject puntoDisparo;
    public Vector3 dirDisp = Vector3.left;
    public float vel = 14;
    public GameObject luz;

    public bool LuzActiva = false;
    public bool MasFuerza=false;
    public bool MasVelocidad = false;
    public bool MasSalto = false;
    public bool Regenera = false;

    public bool tengoPasivaFuerza = false;
    public bool tengoPasivaVelocidad = false;
    public bool tengoPasivaSalto = false;
    public bool tengoPasivaLuz = false;
    public bool tengoRegenera = false;

    public GameObject HUBsalto;
    public GameObject HUBfuerza;
    public GameObject HUBvelocidad;
    public GameObject HUBbrillo;
    public GameObject HUBbase;
    public GameObject HUBregenera;
    

    public GameObject HUBsaltopop;
    public GameObject HUBfuerzapop;
    public GameObject HUBvelocidadpop;
    public GameObject HUBbrillopop;
    public GameObject HUBregenerapop;

    public GameObject HUBsaltoActiva;
    public GameObject HUBfuerzaActiva;
    public GameObject HUBvelocidadActiva;
    public GameObject HUBbrilloActiva;
    public GameObject HUBregeneraActiva;

    public float vida=800;
    public IA IA;
    public int maxVida = 800;
    public Image barraVida;
    public Image barracargada;
    public float velVIDA;
    public float velCARGADA;
    
    public bool tengoLLave = false;
    public int fuerza = 1;
    public float contador;
    public float tiempo;
    public float cadencia;
    public float tiempocadencia = 1.5f;

    public Transform checkpoint;
    public GameObject player;



    public SaltoDoble script3;

    public Movimiento script2;

    public bool MUERTO;

    

    public Animator ataque;

    public Animator ataqueCargado;


    public Movimiento correr;

    public PausarJuego parar;


    public GameObject mensajeESCAPE;




    private void Start()
    {
        mensajeESCAPE.SetActive(true);

        dirDisp = Vector3.right;

        vida = 800;
        
    }

   


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            dirDisp = Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dirDisp = Vector3.right;
        }
        if (vida <= 100)
        {
            
            barraVida.fillAmount = 0.8f;
            

        }
        if(vida<=100)
        {
            MUERTO = true;
           
            script3.vel = 0;
        }

        Disparar();

        Velocidad();

        Fuerza();

        Salto();

        RegeneraVida();

        if(luz == true)
        {
            Brillar();
        }

        if (vida < 800 && Regenera == true)
        {
            vida += 0.13f;
            barraVida.fillAmount += velVIDA;
        }
        
        



        cadencia = cadencia + Time.deltaTime;
    }
   
    //ataque normal
    public void Disparar()
    {


        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            correr.vel = 0;
            contador = contador + Time.deltaTime;
            if(contador>= 0.2f)
            {
                barracargada.fillAmount += velCARGADA;
                ataqueCargado.SetBool("ATAQUECARGADO", true);
                
            }
            
        }

       

        if (Input.GetKeyUp(KeyCode.Mouse0) && contador >= 0.8f)
        {



            
            contador = contador + Time.deltaTime;
            StartCoroutine("ataCargado");
            contador = 0;
            



        }
        if (contador>tiempo && Input.GetKeyUp(KeyCode.Mouse0) && contador <0.8f)
        {

            StartCoroutine("paradaDisparo");
            contador = contador + Time.deltaTime;
            if(cadencia >tiempocadencia)
            {
                StartCoroutine("disparo");
                contador = 0;
                ataqueCargado.SetBool("ATAQUECARGADO", false);
                cadencia = 0;
                ataque.SetBool("ATAQUE", true);
                correr.corriendo.SetBool("CORRIENDO", false);
               
            }
           
            contador = 0;
        }
        









    }
    public IEnumerator ataCargado()
    {
       
        yield return new WaitForSeconds(0.1f);
        ataqueCargado.SetBool("ATAQUECARGADO", false);
        ataque.SetBool("ATAQUE", true);
        
        yield return new WaitForSeconds(0.3f);
        GameObject newBala = (GameObject)Instantiate(balaCargada, puntoDisparo.transform.position, this.transform.rotation);
        newBala.GetComponent<Rigidbody>().linearVelocity = dirDisp * vel;
        Destroy(newBala, 1.5f);
        yield return new WaitForSeconds(0.6f);
        correr.vel = 6;

        yield return new WaitForSeconds(0.1f);
        barracargada.fillAmount = 0;
        yield return new WaitForSeconds(0.1f);
        ataque.SetBool("ATAQUE",false);
       
    }
    public IEnumerator disparo()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject newBala = (GameObject)Instantiate(balaOriginal, puntoDisparo.transform.position, this.transform.rotation);
        newBala.GetComponent<Rigidbody>().linearVelocity = dirDisp * vel;
        Destroy(newBala, 1.5f);
        yield return new WaitForSeconds(0.1f);
        ataque.SetBool("ATAQUE", false);
        barracargada.fillAmount = 0;
    }
   

    public IEnumerator paradaDisparo()
    {
        yield return new WaitForSeconds(0.1f);
        correr.vel = 0;
        yield return new WaitForSeconds(0.5f);
        correr.vel = 6;
    }


    //coger pasivas
    public void OnTriggerEnter(Collider other)
    {

        

        if(other.tag == "PasivaFuerza")
        {
            tengoPasivaFuerza = true;
           
            HUBfuerzapop.SetActive(true);
            Destroy(other.gameObject);
            HUBfuerzaActiva.SetActive(true);
            MasFuerza = false;
            



        }
        if(other.tag == "PasivaVelocidad")
        {
            
            tengoPasivaVelocidad = true;
            HUBvelocidadpop.SetActive(true);
            Destroy(other.gameObject);
            HUBvelocidadActiva.SetActive(true);
            MasVelocidad = false;
            

        }
        if (other.tag == "PasivaSalto")
        {
            HUBsaltopop.SetActive(true);
            
            Destroy(other.gameObject);
            tengoPasivaSalto = true;
            HUBsaltoActiva.SetActive(true);
            
            MasSalto = false;
            

        }
        if (other.tag == "PasivaLuz")
        {
            HUBbrillopop.SetActive(true);
            Destroy(other.gameObject);
            tengoPasivaLuz = true;
            HUBbrilloActiva.SetActive(true);
            LuzActiva = false;
           
            

        }
        if (other.tag == "PasivaVida")
        {

            HUBregenerapop.SetActive(true);
            HUBregeneraActiva.SetActive(true);
            Destroy(other.gameObject);
            tengoRegenera = true;
            parar.pausado = true;
            Regenera = false;
           

        }








    }
   
    //sistema de trampas y puertas
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Trampa")
        {
            barraVida.fillAmount -= 0.15f;
            vida -= 200;
        }
        if (other.gameObject.tag == "Puerta" && tengoLLave == true)
        {
            
            other.gameObject.transform.Translate(0, 5, 0);
        }
        if (other.gameObject.tag == "Enemy")
        {
            barraVida.fillAmount -= 0.15f;
            vida -= 200;
        }
        if (other.gameObject.tag == "Araña")
        {
            barraVida.fillAmount -= 0.15f;
            vida -= 200;
        }
        
    }


    //pasiva de velocidad
    public void Velocidad()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && tengoPasivaVelocidad == true && MasVelocidad == false)
        {
            MasVelocidad = true;
            script2.vel = 13;
            MasFuerza = false;
            luz.SetActive(false);
            MasSalto = false;
            LuzActiva = false;
            HUBvelocidad.SetActive(true);
            HUBfuerza.SetActive(false);
            HUBbrillo.SetActive(false);
            HUBsalto.SetActive(false);
            HUBregenera.SetActive(false);
            script3.vel = 13;
            Regenera = false;
        }
      
            
    }

    //pasiva de fuerza
    public void Fuerza()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && tengoPasivaFuerza == true && MasFuerza == false)
        {
            MasFuerza = true;
            fuerza = 2;
            MasVelocidad = false;
            MasSalto = false;
            luz.SetActive(false);
            script3.vel = 13;
            LuzActiva = false;
            HUBvelocidad.SetActive(false);
            HUBfuerza.SetActive(true);
            HUBbrillo.SetActive(false);
            HUBsalto.SetActive(false);
            HUBregenera.SetActive(false);
            script2.vel = 6;
            Regenera = false;
        }

            
      
    }
    //pasiva de salto
    public void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && tengoPasivaSalto == true && MasSalto == false)
        {
            MasSalto = true;

            script3.vel = 16;

            MasFuerza = false;
            Regenera = false;
            MasVelocidad = false;
            luz.SetActive(false);
            LuzActiva = false;
            HUBvelocidad.SetActive(false);
            HUBfuerza.SetActive(false);
            HUBbrillo.SetActive(false);
            HUBsalto.SetActive(true);
            HUBbase.SetActive(false);
            HUBregenera.SetActive(false);
            script2.vel = 6;

        }


       

    }
    //pasiva de luz
    public void Brillar()
    {

        if (Input.GetKeyDown(KeyCode.Alpha5) && tengoPasivaLuz==true && LuzActiva == false)
        {
            luz.SetActive(true);
            Regenera = false;
            LuzActiva = true;
            MasSalto = false;
            MasFuerza = false;
            MasVelocidad = false;
            HUBvelocidad.SetActive(false);
            HUBfuerza.SetActive(false);
            HUBbrillo.SetActive(true);
            HUBsalto.SetActive(false);
            HUBregenera.SetActive(false);
            script3.vel = 13;
            script2.vel = 6;
        }

      




    }
    //pasiva regenera vida
    public void RegeneraVida()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && tengoRegenera == true && Regenera == false)
        {
            
            Regenera = true;
            
            script3.vel = 13;
            script2.vel = 6;
            LuzActiva = false;
            MasSalto = false;
            MasFuerza = false;
            MasVelocidad = false;
            
            HUBvelocidad.SetActive(false);
            HUBfuerza.SetActive(false);
            HUBbrillo.SetActive(false);
            HUBsalto.SetActive(false);
            HUBregenera.SetActive(true);
            HUBbase.SetActive(false);
        }
    }



}

