using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasCaen : MonoBehaviour
{
    // Start is called before the first frame update
    public int vel;
    public bool Detecta =false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Detecta==true)
        {
            StartCoroutine("esperar");
        }
        
    }
    IEnumerator esperar()
    {
        yield return new WaitForSeconds(1.5f);
        this.transform.Translate(0, vel * Time.deltaTime, 0);
       


        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Detecta = true;
        }
    }
}
