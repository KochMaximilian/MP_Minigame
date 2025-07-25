using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;
    Vector3 desfase;
    // Use this for initialization
    void Start()
    {
        desfase = transform.position - objetivo.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posicionObjeto = objetivo.position + desfase;
        transform.position = Vector3.Lerp(transform.position, posicionObjeto, suavizado * Time.deltaTime);
    }
}
