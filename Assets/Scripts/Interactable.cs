using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected float interactionDistance;
    [SerializeField] protected GameObject interactView;
    [SerializeField] protected KeyCode interactKey;

    public virtual void Awake()
    {
        gameObject.layer = 6;
    }

    public void Update()
    {
        HandleInteractCheck();
    }

    public virtual void OnInteract()
    {
        
    }

    public virtual void OnFocus()
    {
        if (Input.GetKeyDown(interactKey))
            OnInteract();
        //código pra UI
    }

    public virtual void OnLoseFocus()
    {
        
    }

    protected virtual void HandleInteractCheck()
    {
        if (MouseLook.IsLookingToObject(this, interactionDistance))
            OnFocus();
        else
            OnLoseFocus();
    }
}

