using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class HostAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField codeInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(codeInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(codeInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Room1");
    }
}
