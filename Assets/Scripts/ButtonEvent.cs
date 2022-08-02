using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : Interactable
{
    [SerializeField] private bool isSolution = default;

    public override void OnFocus(){}

    public override void OnInteract()
    {
        GameObject.FindWithTag("GameController").GetComponent<Room1Manager>().CheckForSolution(isSolution);
    }

    public override void OnLoseFocus(){}
}
