using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScript : MonoBehaviour
{
    public float rotateSpeed;

    new Camera camera;
    Vector3 touchStart;
    Vector2 center;

    void Start()
    {
        camera = GetComponent<Camera>();
        center.x = Screen.height / 2;
        center.y = Screen.width / 2;
    }

    void Update()
    {
        /*if(Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {

            }
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = Input.mousePosition - touchStart;
            direction.Normalize();
            transform.RotateAround(Vector3.zero, Vector3.up, direction.x * rotateSpeed);
            transform.RotateAround(Vector3.zero, Vector3.right, direction.y * rotateSpeed);
        }
    }
}
