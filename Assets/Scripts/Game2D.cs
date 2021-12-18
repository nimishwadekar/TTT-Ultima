using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber
{
    Player1,
    Player2
}

public class Game2D : MonoBehaviour
{
    public GameObject cell;
    public GameObject player1, player2;

    public ViewScript viewScript;
    public SelectScript selectScript;
    public Button selectButton;

    void Awake()
    {
        selectScript.enabled = false;
    }

    void Start()
    {
        int dim = 3;
        CreateBoard(Vector3.zero, dim);
    }

    public void CreateBoard(Vector3 location, int dimension)
    {
        int lb = Mathf.RoundToInt(dimension / 2.0f) - dimension;
        for(int i = 0; i < dimension; i++)
        {
            for(int j = 0; j < dimension; j++)
            {
                GameObject newCell = Instantiate(cell);
                newCell.transform.position = location + new Vector3(lb + i, 0, lb + j);
                newCell.GetComponent<Cell2D>().location = new System.Tuple<int, int>(i, j);
            }
        }
    }

    public void SelectMode()
    {
        viewScript.enabled = false;
        selectButton.gameObject.SetActive(false);
        selectScript.enabled = true;
        selectScript.BeginSelection(new Vector3(0, 5, 0));
    }
}
