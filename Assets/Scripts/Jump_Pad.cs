using UnityEngine;
using System.Collections;
public class Jumppad : MonoBehaviour
{
    public Vector3 jump;
    public float jumpBoost = 10.0f;
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    public Rigidbody rb;
    private void OnTriggerEnter(Collider pad)
    {
     if(pad.CompareTag("Jumppad"))
        {

            rb.AddForce(jump * jumpBoost, ForceMode.Impulse);
        }      
    }
}
