using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Interactable instance;
    [SerializeField] protected float interactionDistance;
    [SerializeField] protected GameObject interactView;
    [SerializeField] protected KeyCode interactKey;

    public virtual void Awake()
    {
        if (instance == null)
            instance = this;

        instance.gameObject.layer = 6;
    }

    public void Update()
    {
        //HandleInteractCheck();
    }

    public virtual void OnInteract()
    {

    }

    public virtual void OnFocus()
    {
        Debug.Log("onfocus");
        if (Input.GetKeyDown(interactKey))
            instance.OnInteract();
        //código pra UI
    }

    public virtual void OnLoseFocus()
    {

    }

    /*protected virtual void HandleInteractCheck()
    {
        if (MouseLook.IsLookingToObject(this, interactionDistance))
            OnFocus();
        else
            OnLoseFocus();
    }*/

    public virtual Interactable GetInstance()
    {
        return instance;
    }
}

