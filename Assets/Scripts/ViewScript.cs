using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScript : MonoBehaviour
{
    public float rotateSpeed;

    Vector3 touchStart;

    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && touchStart != Input.mousePosition)
        {
            int direction = (touchStart - Input.mousePosition).x > 0 ? 1 : -1;
            transform.RotateAround(Vector3.zero, Vector3.down, direction * rotateSpeed * Time.deltaTime);
        }
    }
}
