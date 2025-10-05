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
    private void Update()
    {
        if (in_range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                SceneManager.LoadScene(Lvl);

            }
        }
    }
        void OnTriggerEnter(Collider touch)
        {
            if (touch.CompareTag("Player") && gameObject.CompareTag("Portal"))
            {
                SceneManager.LoadScene(Lvl);
                
            }

            if (touch.CompareTag("Player") && gameObject.CompareTag("Heart"))
            {

                UI.SetActive(true);
                in_range = true;
            }
            else { UI.SetActive(false); }

        }


            
}
