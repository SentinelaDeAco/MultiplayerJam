using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    [SerializeField] private GameObject interactView;

    public override void OnFocus()
    {}

    public override void OnInteract()
    {
        interactView.SetActive(true);
    }

    public override void OnLoseFocus()
    {}
}
