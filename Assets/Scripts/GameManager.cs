using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerIns;

    [SerializeField] private GameObject endScreen;

    public void GameOver()
    {
        print("game over is called");
        playerIns.gameIsRunning = false;
        endScreen.SetActive(true);
    }
}
