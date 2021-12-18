using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    bool isFilled();
    void ChooseCell(PlayerNumber player);
}
