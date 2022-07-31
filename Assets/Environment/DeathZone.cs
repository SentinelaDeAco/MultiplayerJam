using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            FindObjectOfType<PlayerController>().KillPlayer();
    }
}
