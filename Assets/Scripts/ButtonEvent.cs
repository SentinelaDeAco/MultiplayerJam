using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : Interactable
{
    [SerializeField] private bool isSolution = default;

    public override void OnInteract()
    {
        float closestDist = 1000f;
        PlayerController closestPlayer = null;

        foreach (PlayerController player in GameManager.playerList)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance < closestDist)
            {
                closestPlayer = player;
                closestDist = distance;
            }
        }

        if (closestPlayer != null)
            Actions.OnButtonPress(isSolution, closestPlayer);

        this.gameObject.GetComponent<Animator>().SetTrigger("Pull");
    }
}
