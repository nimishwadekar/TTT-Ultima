using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectScript : MonoBehaviour
{
    public void BeginSelection(Vector3 camPos)
    {
        transform.DORotate(new Vector3(90, -90, 0), 2);
        transform.DOMove(camPos, 2);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                ICell cell = hit.collider.GetComponent<ICell>();
                if(!cell.isFilled())
                {
                    cell.ChooseCell(PlayerNumber.Player1);
                }
            }
        }
    }
}
