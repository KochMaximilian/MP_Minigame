using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaFinal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("exitCinematica");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public IEnumerator exitCinematica()
    {
        yield return new WaitForSeconds(28f);
        SceneManager.LoadScene(5);
    }
}

