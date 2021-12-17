using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board2D : MonoBehaviour
{
    public GameObject cell;
    public GameObject player1, player2;

    void Start()
    {
        int dim = 3;
        CreateGrid(Vector3.zero, dim);
    }

    void CreateGrid(Vector3 location, int dimension)
    {
        int lb = Mathf.RoundToInt(dimension / 2.0f) - dimension;
        for(int i = 0; i < dimension; i++)
        {
            for(int j = 0; j < dimension; j++)
            {
                GameObject newCell = Instantiate(cell);
                newCell.transform.position = location + new Vector3(lb + i, 0, lb + j);
            }
        }
    }
}
