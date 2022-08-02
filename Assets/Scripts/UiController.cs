using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject crosshair = default;
    [SerializeField] private GameObject deathScreen = default;
    [SerializeField] private GameObject successText = default;

    public void SetDeathScreen(bool state)
    {
        deathScreen.SetActive(state);
    }

    public void SetSuccessText(bool state)
    {
        successText.SetActive(state);
    }
}
