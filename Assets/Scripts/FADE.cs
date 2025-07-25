using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


 



public class FADE : MonoBehaviour
{
    public static FADE instancia { get; private set; }
    public Image ImagenCarga;
    [Range(0.01f, 0.5f)]
    public float velocidadAparecer = 0.5f;
    [Range(0.001f, 0.5f)]
    public float velocidadOcultar = 0.5f;
    public AudioSource menu;
    



    void Awake()
    {
        DefinirSingleton();
    }


    private void DefinirSingleton()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
            ImagenCarga.gameObject.SetActive(false);
        }

    }
    public void CargaEscena(string nombreEscena)
    {
        StartCoroutine(MostrarPantallaDeCarga(nombreEscena));
        menu.Pause();

    }
    private IEnumerator MostrarPantallaDeCarga(string nombreEscena)
    {
        ImagenCarga.gameObject.SetActive(true);
        Color c = ImagenCarga.color;
        c.a = 0.0f;
        while (c.a < 1)
        {
            ImagenCarga.color = c;
            c.a += velocidadAparecer;
            yield return null;
        }
        SceneManager.LoadScene(nombreEscena);
        
        while (!nombreEscena.Equals(SceneManager.GetActiveScene().name))
        {
            yield return null;
        }
        while (c.a > 0)
        {
            ImagenCarga.color = c;
            c.a -= velocidadOcultar;
            yield return null;
        }
        DontDestroyOnLoad(this.gameObject);
        ImagenCarga.gameObject.SetActive(false);


    }
}


