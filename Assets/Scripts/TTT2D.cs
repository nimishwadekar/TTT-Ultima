using System;

public enum WinState
{
    Continue,
    Player1Win,
    Player2Win,
    Draw
}

public interface ITTT
{
    WinState Insert(IVec location, PlayerNumber player);
}

public class TTT2D : ITTT
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

    public WinState Insert(IVec location, PlayerNumber player)
    {
        return Insert(location as Vec2, player);
    }

    WinState Insert(Vec2 location, PlayerNumber player)
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

    bool CheckRowWin(Vec2 lastInsert, PlayerNumber player)
    {
        for (int i = 0; i < dimension; i++) if (board[lastInsert.Item1, i] != player) return false;
        return true;
    }

    bool CheckColumnWin(Vec2 lastInsert, PlayerNumber player)
    {
        for (int i = 0; i < dimension; i++) if (board[i, lastInsert.Item2] != player) return false;
        return true;
    }

    bool CheckLeftDiagonalWin(Vec2 lastInsert, PlayerNumber player)
    {
        if (lastInsert.Item1 != lastInsert.Item2) return false;
        for (int i = 0; i < dimension; i++) if (board[i, i] != player) return false;
        return true;
    }

    bool CheckRightDiagonalWin(Vec2 lastInsert, PlayerNumber player)
    {
        if (lastInsert.Item1 + lastInsert.Item2 != dimension - 1) return false;
        for (int i = 0; i < dimension; i++) if (board[i, dimension - i - 1] != player) return false;
        return true;
    }
}
