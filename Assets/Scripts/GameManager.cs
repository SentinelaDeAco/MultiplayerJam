using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    [SerializeField] protected int room;
    [SerializeField] protected GameObject ui;

    protected abstract void OnVictory();
    protected abstract void OnFail();
}
