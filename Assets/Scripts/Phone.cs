using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject ringing;
    public GameObject convo;
    bool pickUp = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (pickUp && Input.GetKey(KeyCode.E))
        {
            ringing.SetActive(false);
            convo.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider touch)
    {
 
        if (touch.CompareTag("Player"))
        {
            pickUp = true;


        }

    }
}
