﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHABILIDAD : MonoBehaviour
{
    public GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
         



        }
    }
    public void exitPanel()
    {
        
        
            panel.SetActive(false);
            
            


        
    }
}
