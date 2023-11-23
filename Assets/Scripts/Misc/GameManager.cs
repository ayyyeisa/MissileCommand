/********************************************************************
// File Name : GameManager.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description :  This is a file that handles when the game ends.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerIns;
    [SerializeField] private GameObject endScreen;

    /// <summary>
    /// Is called on when all buildings have been destroyed. It stops game from running, therefore
    /// preventing coroutines from continuing and gameobject movements.
    /// </summary>
    public void GameOver()
    {
        playerIns.gameIsRunning = false;
        endScreen.SetActive(true);
    }
}
