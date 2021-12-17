using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScript : MonoBehaviour
{
    public void BeginSelection(Vector3 camPos)
    {
        transform.position = camPos;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        print("here");
    }
}
