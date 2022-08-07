using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : GameManager
{
    protected override void OnEnable()
    {    
        base.OnEnable();
        Actions.OnButtonPress += CheckForSolution;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
