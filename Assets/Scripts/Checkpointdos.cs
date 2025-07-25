using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpointdos : MonoBehaviour
{
    private Checkpoint cp;
    // Start is called before the first frame update
    void Start()
    {
        cp = GameObject.FindGameObjectWithTag("CP").GetComponent<Checkpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cp.posicionCheckpoint = transform.position;
        }
    }
}
