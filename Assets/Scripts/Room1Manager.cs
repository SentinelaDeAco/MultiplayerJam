using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : GameManager
{
    private void OnEnable()
    {
        Actions.OnButtonPress += CheckForSolution;
    }

    private void OnDisable()
    {
        Actions.OnButtonPress -= CheckForSolution;
    }

    public void CheckForSolution(bool isSolution, PlayerController player)
    {
        if (isSolution)
            OnVictory();
        else
            OnFailure(player);
    }
}
