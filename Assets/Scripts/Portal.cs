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

    bool in_range = false;
    bool timer;
    public float countDown;
    public float doneCounting = 3.0f;


    private void Update()
    {
        if (in_range)
        {
            if (Input.GetKey(KeyCode.E))
            {
                timer = true;
            }
        }

        Timer(timer);
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
    void Timer(bool active)
    {
        if (active)
        {
            countDown += Time.deltaTime;
            if (countDown >= doneCounting)
            {
                SceneManager.LoadScene(Lvl);
                countDown = 0;
            }
        }
    }

    }
   