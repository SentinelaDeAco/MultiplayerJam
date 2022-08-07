using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class GameManager : MonoBehaviour
{
    [SerializeField] protected int room;
    [SerializeField] protected UiController ui;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointJ2;
    [SerializeField] private Transform spawnPointR1;
    public static List<PlayerController> playerList;

    protected void Start()
    {
        playerList = new List<PlayerController>();
        SpawnPlayer();
    }

    private void Update()
    {

        Debug.Log(playerList.Count);
    }

    protected virtual void OnEnable()
    {
        Actions.OnPlayerJoin += OnPlayerJoin;
        Actions.OnPlayerRespawn += RestartGame;
        Actions.OnPlayerDeath += OnFailure;
        Actions.OnPlayerLeave += OnPlayerLeave;
    }

    protected virtual void OnDisable()
    {
        Actions.OnPlayerJoin -= OnPlayerJoin;
        Actions.OnPlayerRespawn -= RestartGame;
        Actions.OnPlayerDeath -= OnFailure;
        Actions.OnPlayerLeave -= OnPlayerLeave;
    }

    protected virtual void OnVictory() 
    {
        ui.GetComponent<UiController>().SetSuccessText(true);
    }

    public virtual void OnFailure(PlayerController player)
    {
        ui.SetDeathScreen(true);
        player.KillPlayer();
    }

    public virtual void SpawnPlayer()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);

        /*if (playerList.Count == 0)
            spawnPos = spawnPointR1.position;
        else
            spawnPos = spawnPointJ2.position;*/

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }

    public virtual void PositionPlayer(int id)
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);

        /*if (playerList.Count == 0)
            spawnPos = spawnPointR1.position;
        else
            spawnPos = spawnPointJ2.position;*/

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }

    public virtual void RestartGame()
    {
        ui.SetDeathScreen(false);
        foreach (PlayerController player in playerList)
            player.RespawnPlayer(spawnPointJ2);
    }

    public static void OnPlayerJoin(PlayerController player)
    {
        playerList.Add(player);
    }

    public static void OnPlayerLeave(PlayerController player)
    {
        playerList.Remove(player);
    }
}
