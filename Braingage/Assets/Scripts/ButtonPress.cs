using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonPress : MonoBehaviour
{
    public GameObject smallCircle;


    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && firstTime == true) {
            firstTime = false;
            smallCircle.SetActive(true);
            StartCoroutine(ExampleCoroutine());
        }
    }

    /*
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {

             //smallCircle.SetActive(false);
        }
    }*/

    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("HelloCube_handtracking");
    }
}
