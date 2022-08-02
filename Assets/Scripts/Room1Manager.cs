using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : GameManager
{
    protected override void OnVictory()
    {
        ui.GetComponent<UiController>().SetSuccessText(true);
    }

    protected override void OnFail()
    {
        FindObjectOfType<PlayerController>().KillPlayer();
    }

    public void CheckForSolution(bool isSolution)
    {
        if (isSolution)
            OnVictory();
        else
            OnFail();
    }
}
