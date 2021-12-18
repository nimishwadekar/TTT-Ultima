using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cell2D : MonoBehaviour, ICell
{
    public Action<Tuple<int, int>> InsertAction;

    public Tuple<int, int> location;
    public GameObject player1Piece, player2Piece;

    bool filled = false;

    public void ChooseCell(PlayerNumber player)
    {
        GameObject piece = player == PlayerNumber.Player1 ? player1Piece : player2Piece;
        Instantiate(piece, transform);
        filled = true;
        InsertAction(location);
    }

    public bool isFilled() => filled;
}
