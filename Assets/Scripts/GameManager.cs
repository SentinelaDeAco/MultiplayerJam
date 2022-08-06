using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    [SerializeField] protected int room;
    [SerializeField] protected UiController ui;
    [SerializeField] protected GameObject respawnPoint;
    public static List<PlayerController> playerList;

    protected void Start()
    {
        playerList = new List<PlayerController>();
    }

    private void OnEnable()
    {
        Actions.OnPlayerJoin += OnPlayerJoin;
        Actions.OnPlayerRespawn += RestartGame;
        Actions.OnPlayerDeath += OnFailure;
        Actions.OnPlayerLeave += OnPlayerLeave;
    }

    private void OnDisable()
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

    public virtual void RestartGame()
    {
        ui.SetDeathScreen(false);
        foreach (PlayerController player in playerList)
            player.RespawnPlayer(respawnPoint.transform);
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
