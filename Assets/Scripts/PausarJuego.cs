using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject pausePanel;
    public bool pausado=false;
    public GameObject estasSeguro;

   
    
    
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        pauseGame();
        
    }
    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausado == false)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;

            pausado = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)&& pausado ==true)
        {
            pausePanel.SetActive(false);

            Time.timeScale = 1;
            pausado = false;
            
        }



    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pausado = false;
        


    }
    public void Exit()
    {
        Application.Quit();


    }
    public void Seguro()
    {
        estasSeguro.SetActive(true);
    }
    public void noSeguro()
    {
        estasSeguro.SetActive(false);
    }
}
