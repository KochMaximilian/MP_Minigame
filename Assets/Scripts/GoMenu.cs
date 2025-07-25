using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject menu2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Go()
    {
        menu.SetActive(true);
        menu2.SetActive(false);
        Time.timeScale = 0;

    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void carga()
    {
        SceneManager.LoadScene(2);
    }
}
