using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampasCristales : MonoBehaviour
{
    public GameObject cristales;
    // Start is called before the first frame update
    void Start()
    {
        cristales.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player")
        {
            StartCoroutine("cristal");
            

        }
    }
    public IEnumerator cristal()
    {
        yield return new WaitForSeconds(0.3f);
        Rigidbody cris = cristales.GetComponent<Rigidbody>();
        cris.useGravity = true;

    }
}
