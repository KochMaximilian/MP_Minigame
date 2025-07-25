using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaFlecha : MonoBehaviour
{
    public GameObject Flecha;
    public float contador;
    public float limite = 2;
    public int min = 1;
    public int max = 6;


    public Vector3 dir = Vector3.right;

    // Use this for initialization
    void Start()
    {
        CalculoAleatorio();
    }

    // Update is called once per frame
    void Update()
    {
        contador = contador + Time.deltaTime;

        if (contador > limite)
        {
            GameObject nuevaFlecha = (GameObject)Instantiate(Flecha, this.transform.position, this.transform.rotation);
            

            contador = 0;
            CalculoAleatorio();
        }

    }
    void CalculoAleatorio()
    {
        limite = Random.Range(min, max);
    }
}
