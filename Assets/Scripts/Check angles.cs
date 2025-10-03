using UnityEngine;

public class Checkangles : MonoBehaviour
{

    public float angle;

    // Update is called once per frame
    void Update()
    {
        angle = Camera.main.transform.localEulerAngles.x;
    }
}
