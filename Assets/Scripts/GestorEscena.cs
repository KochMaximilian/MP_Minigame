using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscena : MonoBehaviour
{
    public bool isLogo;
    public float time=5f;
    private float tiempoInicio = 0f;
    // Start is called before the first frame update
    void Start()
    {
        isLogo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLogo)
        {
            tiempoInicio += Time.deltaTime;
            if(tiempoInicio>= time)
            {
                GotoScene(1);
                tiempoInicio = 0;
            }
        }
    }
    public void GotoScene(int n)
    {
        SceneManager.LoadScene(1);
    }
}
