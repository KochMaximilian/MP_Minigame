﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    public AtaqueDistancia player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.vida = 0;
            player.MUERTO = true;
        }
    }
}
