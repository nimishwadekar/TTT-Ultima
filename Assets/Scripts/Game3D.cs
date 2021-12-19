using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Game3D : Game2D
{
    public float layerMoveTime;
    public Button downButton, upButton;

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
        upButton.gameObject.SetActive(false);
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

    public void UpButton()
    {
        StartCoroutine(LayerLerp(false));
    }

    public void DownButton()
    {
        StartCoroutine(LayerLerp(true));
    }

    IEnumerator LayerLerp(bool down)
    {
        int neg = down ? 1 : -1;

        viewScript.enabled = false;
        viewComponents.SetActive(false);

        if (!down) topLayer += 1;

        for (int j = 0; j < dimension; j++)
        {
            for (int k = 0; k < dimension; k++)
            {
                Transform cellTransform = cells[topLayer][j][k].transform;
                cellTransform.DOMove(cellTransform.position + neg * new Vector3(dimension, 0, 0), layerMoveTime);
            }
        }
        yield return new WaitForSeconds(layerMoveTime);

        viewComponents.SetActive(true);
        viewScript.enabled = true;

        if(down)
        {
            topLayer -= 1;
            if (topLayer == dimension - 2) upButton.gameObject.SetActive(true);
            else if (topLayer == 0) downButton.gameObject.SetActive(false);
        }
        else
        {
            if (topLayer == 1) downButton.gameObject.SetActive(true);
            else if (topLayer == dimension - 1) upButton.gameObject.SetActive(false);
        }
    }
}
