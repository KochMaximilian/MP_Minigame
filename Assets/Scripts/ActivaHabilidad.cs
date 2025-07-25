using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaHabilidad : MonoBehaviour
{
    public Animator AnimacionPetalos;
    public GameObject petalos;
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
            AnimacionPetalos.SetBool("PETALOS", true);
            petalos.GetComponent<SphereCollider>();
            SphereCollider peta = petalos.GetComponent<SphereCollider>();
            peta.isTrigger = true;
        }
    }
}
