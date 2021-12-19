using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;

public class Game3D : Game2D
{
    public float rotateTime;

    GameObject[][][] cells;
    int topLayer, dimension;

    void Start()
    {
        int dim = 3;

        cameraBasePosition = Camera.main.transform.position;
        cameraBaseRotation = Camera.main.transform.rotation.eulerAngles;

        dimension = dim;
        playerTurn = PlayerNumber.Player1;
        ttt = new TTT3D(dim);
        topLayer = dim - 1;
        CreateBoard(dim);
    }

    void CreateBoard(int dimension)
    {
        cells = new GameObject[dimension][][];
        for(int i = 0; i < dimension; i++)
        {
            cells[i] = new GameObject[dimension][];
            for (int j = 0; j < dimension; j++) cells[i][j] = new GameObject[dimension];
        }

        float lb = (dimension + 1) / 2.0f - dimension;
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for(int k = 0; k < dimension; k++)
                {
                    GameObject newCell = Instantiate(cell);
                    newCell.transform.position = new Vector3(lb + j, lb + i, lb + k);
                    Cell cell3D = newCell.GetComponent<Cell>();
                    cell3D.location = new Vec3(i, j, k);
                    cell3D.InsertAction += MakeMove;
                    cells[i][j][k] = newCell;
                }
            }
        }
    }

    public void Cycle2DButton()
    {
        StartCoroutine(CycleTTT());
    }

    IEnumerator CycleTTT()
    {
        viewScript.enabled = false;
        viewComponents.SetActive(false);

        for (int j = 0; j < dimension; j++)
        {
            for (int k = 0; k < dimension; k++)
            {
                Transform cellTransform = cells[topLayer][j][k].transform;
                cellTransform.DOMove(cellTransform.position + new Vector3(dimension, 0, 0), rotateTime / 3);
            }
        }
        yield return new WaitForSeconds(rotateTime / 3);

        for(int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    Transform cellTransform = cells[i][j][k].transform;
                    if(i == topLayer)
                    {
                        cellTransform.DOMove(cellTransform.position - new Vector3(0, dimension - 1, 0), rotateTime / 3);
                    }
                    else
                    {
                        cellTransform.DOMove(cellTransform.position + new Vector3(0, 1, 0), rotateTime / 3);
                    }
                }
            }
        }
        yield return new WaitForSeconds(rotateTime / 3);

        for (int j = 0; j < dimension; j++)
        {
            for (int k = 0; k < dimension; k++)
            {
                Transform cellTransform = cells[topLayer][j][k].transform;
                cellTransform.DOMove(cellTransform.position - new Vector3(dimension, 0, 0), rotateTime / 3);
            }
        }
        yield return new WaitForSeconds(rotateTime / 3);

        topLayer = topLayer == 0 ? dimension - 1 : topLayer - 1;

        viewComponents.SetActive(true);
        viewScript.enabled = true;
    }
}
