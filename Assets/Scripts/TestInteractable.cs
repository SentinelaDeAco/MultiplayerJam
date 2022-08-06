using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();
        interactView.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        interactView.SetActive(false);
    }
}
