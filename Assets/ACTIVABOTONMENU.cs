﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTIVABOTONMENU : MonoBehaviour
{
    public GameObject botonmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            botonmenu.SetActive(true);
        }
    }
}
