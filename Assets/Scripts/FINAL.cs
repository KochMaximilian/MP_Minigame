using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FINAL : MonoBehaviour
{
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
            StartCoroutine("finalescena");
        }
    }

    public IEnumerator finalescena()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }
}
