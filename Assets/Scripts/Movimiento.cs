using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float vel = 6;
    
    public float velocidad;

    public Animator corriendo;

    public bool mover;
    // Use this for initialization
    void Start()
    {
        mover = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A ) && mover == true)
        {
            this.transform.Translate(0, 0, -vel * Time.deltaTime);
            this.transform.localScale = new Vector3(1f, 1f, -1f);
            corriendo.SetBool("CORRIENDO", true);
          
        }
      


        else if (Input.GetKey(KeyCode.D) && mover == true)
        {
            this.transform.Translate(0, 0, vel * Time.deltaTime);
            this.transform.localScale = new Vector3(1f, 1f,1f);
            corriendo.SetBool("CORRIENDO", true);
            
        }
        else 
        {
            StartCoroutine("noCorre");

        }
        
    }
   public IEnumerator noCorre()
    {
        yield return new WaitForSeconds(0.1f);
        corriendo.SetBool("CORRIENDO", false);
    }
   
}
