using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT3D : ITTT
{
    public TTT3D(int dim)
    {

    }

    public WinState Insert(IVec location, PlayerNumber player)
    {
        Debug.Log("3d");
        return WinState.Continue;
    }
}
