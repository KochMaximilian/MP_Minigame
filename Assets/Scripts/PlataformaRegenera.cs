using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaRegenera : MonoBehaviour
{
    public Transform posicion;
    public GameObject plataforma;
    public float contador;
    public float tiempo;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        contador = contador + Time.deltaTime;

        if(contador>tiempo)
        {
            plataforma.transform.position = posicion.position;
            plataforma.transform.rotation = posicion.rotation;
            contador = 0;
        }
    }
}
