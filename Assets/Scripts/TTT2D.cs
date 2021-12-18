using System;

public enum WinState
{
    Continue,
    Player1Win,
    Player2Win,
    Draw
}

public class TTT2D
{
    public readonly int dimension;

    public PlayerNumber this[int i, int j]
    {
        get => board[i, j];
    }

    PlayerNumber[,] board;
    int moves;
    readonly int maxMoves;

    public TTT2D(int dim)
    {
        dimension = dim;
        board = new PlayerNumber[dim, dim];
        moves = 0;
        maxMoves = dim * dim;
    }

    public WinState Insert(Tuple<int, int> location, PlayerNumber player)
    {
        board[location.Item1, location.Item2] = player;
        moves += 1;
        if (CheckRowWin(location, player) || CheckColumnWin(location, player) ||
            CheckLeftDiagonalWin(location, player) || CheckRightDiagonalWin(location, player))
        {
            return player == PlayerNumber.Player1 ? WinState.Player1Win : WinState.Player2Win;
        }
        else if (moves == maxMoves) return WinState.Draw;
        return WinState.Continue;
    }

    bool CheckRowWin(Tuple<int, int> lastInsert, PlayerNumber player)
    {
        for (int i = 0; i < dimension; i++) if (board[lastInsert.Item1, i] != player) return false;
        return true;
    }

    bool CheckColumnWin(Tuple<int, int> lastInsert, PlayerNumber player)
    {
        for (int i = 0; i < dimension; i++) if (board[i, lastInsert.Item2] != player) return false;
        return true;
    }

    bool CheckLeftDiagonalWin(Tuple<int, int> lastInsert, PlayerNumber player)
    {
        if (lastInsert.Item1 != lastInsert.Item2) return false;
        for (int i = 0; i < dimension; i++) if (board[i, i] != player) return false;
        return true;
    }

    bool CheckRightDiagonalWin(Tuple<int, int> lastInsert, PlayerNumber player)
    {
        if (lastInsert.Item1 + lastInsert.Item2 != dimension - 1) return false;
        for (int i = 0; i < dimension; i++) if (board[i, dimension - i - 1] != player) return false;
        return true;
    }
}
