﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristales : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Suelo")
        {
            Destroy(this.gameObject);
        }
    }
}
