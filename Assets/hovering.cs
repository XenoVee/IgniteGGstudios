using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hovering : MonoBehaviour
{

    private float x;
    private float z;
    private float startY;
    public float speed = 1f;
    public float amplitude = 0.5f;

    private void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
        startY = transform.position.y;
    }
    private void Update()
    {
        transform.position = new Vector3(x, startY + Mathf.Sin(Time.time*speed)* amplitude, z);
    }
}
