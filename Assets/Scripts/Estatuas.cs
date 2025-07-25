using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estatuas : MonoBehaviour
{
    
    public AtaqueDistancia script;

    public Animator TEX;
    
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            script.tengoLLave = true;
            TEX.SetBool("TEXTURA", true);
        }
      
    }
    
}
