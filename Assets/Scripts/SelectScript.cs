using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectScript : MonoBehaviour
{
    public float cameraMoveTime;

    public PlayerNumber player;

    void Start()
    {
        player = PlayerNumber.Player1;
    }

    public void BeginSelection(Vector3 camPos)
    {
        transform.DORotate(new Vector3(90, -90, 0), cameraMoveTime);
        transform.DOMove(camPos, cameraMoveTime);
    }

    public void ChangePlayer()
    {
        player = player == PlayerNumber.Player1 ? PlayerNumber.Player2 : PlayerNumber.Player1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (!hit.collider.CompareTag("Cell"))
                {
                    return;
                }

                Cell cell = hit.collider.GetComponent<Cell>();
                if(!cell.isFilled())
                {
                    cell.ChooseCell(player);
                }
            }
        }
    }
}
