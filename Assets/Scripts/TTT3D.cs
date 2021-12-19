
public class TTT3D : ITTT
{
    public readonly int dimension;

    PlayerNumber[,,] board;
    int moves;
    readonly int maxMoves;

    public PlayerNumber this[int i, int j, int k]
    {
        get => board[i, j, k];
    }

    public TTT3D(int dim)
    {
        dimension = dim;
        moves = 0;
        maxMoves = dim * dim * dim;

        board = new PlayerNumber[dim, dim, dim];
    }

    public WinState Insert(IVec location, PlayerNumber player)
    {
        return Insert(location as Vec3, player);
    }

    WinState Insert(Vec3 location, PlayerNumber player)
    {
        board[location.Item1, location.Item2, location.Item3] = player;
        moves += 1;
        if (Check1DWin(location, player) || Check2DWin(location, player) || Check3DWin(location, player))
        {
            return player == PlayerNumber.Player1 ? WinState.Player1Win : WinState.Player2Win;
        }
        else if (moves == maxMoves) return WinState.Draw;
        return WinState.Continue;
    }

    bool Check1DWin(Vec3 location, PlayerNumber player)
    {
        bool[] won = { true, true, true };
        for(int i = 0; i < dimension; i++)
        {
            won[0] &= board[i, location.Item2, location.Item3] == player;
            won[1] &= board[location.Item1, i, location.Item3] == player;
            won[2] &= board[location.Item1, location.Item2, i] == player;
        }
        return won[0] || won[1] || won[2];
    }

    bool Check2DWin(Vec3 location, PlayerNumber player)
    {
        bool[] won = { true, true, true , true, true, true };
        for (int i = 0; i < dimension; i++)
        {
            won[0] &= board[location.Item1, i, i] == player;
            won[1] &= board[i, location.Item2, i] == player;
            won[2] &= board[i, i, location.Item3] == player;

            won[3] &= board[location.Item1, i, dimension - 1 - i] == player;
            won[4] &= board[i, location.Item2, dimension - 1 - i] == player;
            won[5] &= board[i, dimension - 1 - i, location.Item3] == player;
        }
        return won[0] || won[1] || won[2] || won[3] || won[4] || won[5];
    }

    bool Check3DWin(Vec3 location, PlayerNumber player)
    {
        bool[] won = { true, true, true, true };
        for (int i = 0; i < dimension; i++)
        {
            won[0] &= board[i, i, i] == player;
            won[1] &= board[dimension - 1 - i, i, i] == player;
            won[2] &= board[i, dimension - 1 - i, i] == player;
            won[3] &= board[i, i, dimension - 1 - i] == player;
        }
        return won[0] || won[1] || won[2] || won[3];
    }
}
