using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;


public class Portal : MonoBehaviour
{
    public string Lvl;
    public GameObject UI;

    public Rigidbody rb;
    bool in_range = false;
    bool timer;
    public int I;
    private void Update()
    {
        if (in_range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(Lvl);

            }
        }
        if (timer)
        {
            I += 1;
            if (I == 300)
            {
                SceneManager.LoadScene(Lvl);
                I = 0;
            }
        }


    }
    void OnTriggerEnter(Collider touch)
    {
        if (touch.CompareTag("Player") && gameObject.CompareTag("Portal"))
        {
            timer = true;


        }

        if (touch.CompareTag("Player") && gameObject.CompareTag("Heart"))
        {

            UI.SetActive(true);
            in_range = true;
        }

    }
    void OnTriggerExit(Collider touch)
    {
        if (touch.CompareTag("Player") && gameObject.CompareTag("Heart"))
        {
            UI.SetActive(false);
            in_range = false;
        }
    }


            
}
