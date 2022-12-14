using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action<PlayerController> OnPlayerJoin;
    public static Action<PlayerController> OnPlayerDeath;
    public static Action OnPlayerRespawn;
    public static Action<PlayerController> OnPlayerLeave;
    public static Action<bool, PlayerController> OnButtonPress;
    public static Action<PlayerController> OnPlayerFalling;
    public static Action<PlayerController> Rspwn1;
    public static Action<PlayerController> Rspwn2;
}
