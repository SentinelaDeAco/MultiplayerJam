using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    [SerializeField] private GameObject interactView;

    public override void OnFocus()
    {
        Debug.Log("LOOKING AT " + gameObject.name);
    }

    public override void OnInteract()
    {
        Debug.Log("INTERACTED WITH " + gameObject.name);
        interactView.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        Debug.Log("STOPPED LOOKING AT " + gameObject.name);
        interactView.SetActive(false);
    }
}
