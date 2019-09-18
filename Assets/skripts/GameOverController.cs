using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameManager gameManager;

    public void OnMouseDown()
    {
        gameManager.RestartGame();
    }
}
