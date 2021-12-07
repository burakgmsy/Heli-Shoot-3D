using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float speed;
    public bool isBig;
    void FixedUpdate()
    {
        if (isBig)
             transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.Self);            
        else
            transform.RotateAround(transform.position, Vector3.right, speed * Time.deltaTime);

    }
}
