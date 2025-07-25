using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaPlegable : MonoBehaviour
{
    public Animator plataforma;
    public bool activado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activado ==true)
        {
            StartCoroutine("animacion");
            
        }
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && activado ==false)
        {
            plataforma.SetBool("Activada", true);
            activado = true;
        }
        
    }
    IEnumerator animacion()
    {
        yield return new WaitForSeconds(1f);
        if (activado == true)
        {
            plataforma.SetBool("Activada", false);
            activado = false;
        }
    }
}
