using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaDer : MonoBehaviour
{
    public float vel = 15;
    public Vector3 dir = Vector3.right;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(dir * -vel * Time.deltaTime, Space.World);
        Destroy(this.gameObject, 2.5f);
    }
    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            
        }

       
    }
}
