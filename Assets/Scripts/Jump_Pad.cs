using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Jumppad : MonoBehaviour
{
    public Player_Movement PlayerMovement;
    public Vector3 jump;
    public float jumpBoost = 10.0f;
    void Start()
    { 
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.jumpHeight += jumpBoost;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.jumpHeight = PlayerMovement.originalJumpHeight;
        }
    }
}

