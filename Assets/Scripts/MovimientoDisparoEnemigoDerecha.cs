using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDisparoEnemigoDerecha : MonoBehaviour
{
    public float vel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.Translate(0, 0, -vel * Time.deltaTime);
        Destroy(this.gameObject, 3f);

    }
    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
