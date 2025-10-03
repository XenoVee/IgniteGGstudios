using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;


public class Portal : MonoBehaviour
{
    public static int Lvl = 1;
    public GameObject UI;

    public Rigidbody rb;
    bool in_range = false;
    private void Update()
    {
        if (in_range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                SceneManager.LoadScene(Lvl);
                Lvl += 1;

            }
        }
    }
        void OnTriggerEnter(Collider touch)
        {
            if (touch.CompareTag("Player") && gameObject.CompareTag("Portal"))
            {
                SceneManager.LoadScene(Lvl);
                Lvl += 1;
            }

            if (touch.CompareTag("Player") && gameObject.CompareTag("Heart"))
            {

                UI.SetActive(true);
                in_range = true;
            }
            else { UI.SetActive(false); }

        }


    
}
