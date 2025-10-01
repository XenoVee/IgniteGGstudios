using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class Portal : MonoBehaviour
{
    public int Lvl = 1;

    public Rigidbody rb;
     void OnTriggerEnter(Collider touch)
    {
        if (touch.CompareTag("Player"))
        {
        SceneManager.LoadScene(Lvl);
        Lvl += 1;
        }
       
    }


}
