using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public enum PlayerNumber
{
    None,
    Player1,
    Player2
}

public class Game2D : MonoBehaviour
{
    public GameObject cell;

    public ViewScript viewScript;
    public SelectScript selectScript;
    public GameObject viewComponents;
    public Button okButton;
    public GameObject winPanel;
    public Text winText;

    public float cameraMoveTime;

    protected ITTT ttt;
    protected PlayerNumber playerTurn;
    protected Vector3 cameraBasePosition, cameraBaseRotation;

    void Awake()
    {
        okButton.gameObject.SetActive(false);
        selectScript.enabled = false;
        winPanel.SetActive(false);
    }

    void Start()
    {
        cameraBasePosition = Camera.main.transform.position;
        cameraBaseRotation = Camera.main.transform.rotation.eulerAngles;

        int dim = 3;
        playerTurn = PlayerNumber.Player1;
        ttt = new TTT2D(dim);
        CreateBoard(dim);
    }

    void CreateBoard(int dimension)
    {
        float lb = (dimension + 1) / 2.0f - dimension;
        for (int i = 0; i < dimension; i++)
        {
            for(int j = 0; j < dimension; j++)
            {
                GameObject newCell = Instantiate(cell);
                newCell.transform.position = new Vector3(lb + i, 0, lb + j);
                Cell cell2D = newCell.GetComponent<Cell>();
                cell2D.location = new Vec2(i, j);
                cell2D.InsertAction += MakeMove;
            }
        }
    }

    public void SelectMode()
    {
        viewScript.enabled = false;
        viewComponents.SetActive(false);
        selectScript.enabled = true;
        selectScript.BeginSelection(new Vector3(0, 5, 0));
    }

    IEnumerator ViewMode()
    {
        selectScript.enabled = false;
        Camera.main.transform.DORotate(cameraBaseRotation, cameraMoveTime);
        Camera.main.transform.DOMove(cameraBasePosition, cameraMoveTime);
        viewScript.BeginViewing();

        yield return new WaitForSeconds(cameraMoveTime);

        viewScript.enabled = true;
        viewComponents.SetActive(true);
    }

    protected virtual void MakeMove(IVec loc)
    {
        WinState state = ttt.Insert(loc, playerTurn);
        if (state == WinState.Continue)
        {
            playerTurn = playerTurn == PlayerNumber.Player1 ? PlayerNumber.Player2 : PlayerNumber.Player1;
            selectScript.ChangePlayer();
            StartCoroutine(ViewMode());
            return;
        }

        selectScript.enabled = false;
        winPanel.SetActive(true);
        winText.text = state.ToString();
        okButton.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
