using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoDoble : MonoBehaviour
{
    public Rigidbody miRigid;
    public float vel =13;
    public bool tocoSuelo = false;
    private bool dobleSalto = false;
    public int numeroSaltos = 0;
    public Animator SALTO;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        tocoSuelo = false;
        RaycastHit resultadoRayo;
        if (Physics.Raycast(this.transform.position, Vector3.down, out resultadoRayo, 1.5f))
        {
            if (resultadoRayo.transform.tag == "Suelo" )
            {
              
                tocoSuelo = true;
                dobleSalto = false;
            }
            else if (resultadoRayo.transform.tag == "Trampa")
            {
                
                tocoSuelo = true;
                dobleSalto = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && (tocoSuelo || !dobleSalto) && numeroSaltos < 1)
        {
            SALTO.SetBool("SALTO", true);
            StartCoroutine("acabaSalto");
            numeroSaltos++;
            miRigid.linearVelocity = Vector3.up * vel;
        }
      




        if (tocoSuelo && numeroSaltos == 1)
        {
            numeroSaltos = 0;
            
        }
        if (tocoSuelo && numeroSaltos < 1)
        {
            numeroSaltos = 0;

        }
        if (numeroSaltos == 1 && tocoSuelo == true)
        {
            numeroSaltos = 0;
        }
    }
    public IEnumerator acabaSalto()
    
    {
        yield return new WaitForSeconds(0.6f);
      SALTO.SetBool("SALTO", false);
    }

}



