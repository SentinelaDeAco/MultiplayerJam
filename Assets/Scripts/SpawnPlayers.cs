using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointOne;
    //[SerializeField] private Transform spawnPointTwo;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPointOne.position, Quaternion.identity);
    }
}
