using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScript : MonoBehaviour
{
    public float rotateSpeedMouse, rotateSpeedTouch;
    public Transform verticalAxis, verticalPole, horizontalAxis, horizontalPole;

    Vector2 touchStart;
    Vector3 baseVerticalAxisPosition, baseVerticalAxisRotation, baseHorizontalAxisPosition, baseHorizontalAxisRotation;

    void Start()
    {
        baseVerticalAxisPosition = verticalAxis.position;
        baseVerticalAxisRotation = verticalAxis.rotation.eulerAngles;
        baseHorizontalAxisPosition = horizontalAxis.position;
        baseHorizontalAxisRotation = horizontalAxis.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        MobileTouch();
        //EditorTouch();
    }

    public void BeginViewing()
    {
        verticalAxis.position = baseVerticalAxisPosition;
        verticalAxis.rotation = Quaternion.Euler(baseVerticalAxisRotation);
        horizontalAxis.position = baseHorizontalAxisPosition;
        horizontalAxis.rotation = Quaternion.Euler(baseHorizontalAxisRotation);
    }

    void MobileTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
            {
                touchStart = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 dir = (touchStart - Input.touches[0].position) / Screen.width;
                Vector3 oldRot = transform.rotation.eulerAngles;
                transform.RotateAround(Vector3.zero, -verticalPole.position, dir.x * rotateSpeedTouch * Time.deltaTime);
                transform.RotateAround(Vector3.zero, -horizontalPole.position, dir.y * rotateSpeedTouch * Time.deltaTime);
                verticalAxis.rotation = Quaternion.Euler(verticalAxis.rotation.eulerAngles + transform.rotation.eulerAngles - oldRot);
                horizontalAxis.rotation = Quaternion.Euler(horizontalAxis.rotation.eulerAngles + transform.rotation.eulerAngles - oldRot);
            }
        }
    }

    void EditorTouch()
    {
        Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = mousePos;
        }
        if (Input.GetMouseButton(0) && touchStart != mousePos)
        {
            int xDir = (touchStart - mousePos).x > 0 ? 1 : -1;
            int yDir = (touchStart - mousePos).y > 0 ? 1 : -1;
            Vector3 oldRot = transform.rotation.eulerAngles;
            transform.RotateAround(Vector3.zero, -verticalPole.position, xDir * rotateSpeedMouse * Time.deltaTime);
            transform.RotateAround(Vector3.zero, -horizontalPole.position, yDir * rotateSpeedMouse * Time.deltaTime);
            verticalAxis.rotation = Quaternion.Euler(verticalAxis.rotation.eulerAngles + transform.rotation.eulerAngles - oldRot);
            horizontalAxis.rotation = Quaternion.Euler(horizontalAxis.rotation.eulerAngles + transform.rotation.eulerAngles - oldRot);
        }
    }
}
