using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Action<IVec> InsertAction;

    public IVec location;
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
