using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ButtonPress_Wrist : MonoBehaviour
{
    public GameObject thisObject;
    public GameObject thisObject2;


    public GameObject menu;
    public GameObject particle478;
    public Animator particleAnim;

    public AudioSource audioclip;
    public AudioSource audioclip_478;

    public GameObject settings;

    public TextMeshProUGUI text;

    bool timerOn = false;
    float time = 0f;
    float time2 = 0f;

    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        //text.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        //if(particle478.activeSelf == true){
            //menu.SetActive(false);
        //}
        time += Time.deltaTime;
        //text.text = time.ToString();
        if(time>=28f){
            particle478.SetActive(false);
        }
        /*
        if(timerOn == false){
                time += Time.deltaTime;
                if(time >= 28f){
                    particle478.SetActive(false);
                    timerOn = false;
                }
                //text.text = time.ToString();

                time2 += Time.deltaTime;
                if(time2 >= 4f && time2 < 9f){
                    text.text = "Breathe in for 4";
                }else if(time2 >= 9f && time2 < 18f){
                    text.text = "Hold for 7";
                }else if(time2 >= 18f && time2 < 28f){
                    text.text = "Exhale for 8";
                }
            }*/
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            if (this.transform.gameObject.name == "energize") {
                //thisObject.SetActive(true);
            }else if (this.transform.gameObject.name == "settings") {
                if(thisObject2.activeSelf == false){
                    thisObject2.SetActive(true);
                    settings.GetComponent<Image>().color = Color.blue;
                    settings.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                }else{
                    thisObject2.SetActive(false);
                    settings.GetComponent<Image>().color = Color.white;
                    settings.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }else if(this.transform.gameObject.name == "478"){
                if(firstTime == true){
                    firstTime = false;
                    timerOn = true;
                    menu.SetActive(false);
                    audioclip.Play();
                    audioclip_478.Play();
                    particle478.SetActive(true);
                    //StartCoroutine(ExampleCoroutine2());
                    //start478();
                }
                    
            }

            
        }
        
    }

    void start478(){
        //particle478.SetActive(true);
        
        //particleAnim.Play();
        StartCoroutine(ExampleCoroutine());
    } 

    /*
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {

             //smallCircle.SetActive(false);
        }
    }*/

    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(19);
        particle478.SetActive(false);
        //menu.SetActive(true);
        //SceneManager.LoadScene("HelloCube_handtracking");
    }

    IEnumerator ExampleCoroutine2()
    {

        yield return new WaitForSeconds(6);
        //particle478.SetActive(true);
        start478();
        //menu.SetActive(true);
        //SceneManager.LoadScene("HelloCube_handtracking");
    }
}
