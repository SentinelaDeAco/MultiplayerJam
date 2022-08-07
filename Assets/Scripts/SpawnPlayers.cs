using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointJ2;
    [SerializeField] private Transform spawnPointR1;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPointJ2.position, Quaternion.identity);
    }
}
