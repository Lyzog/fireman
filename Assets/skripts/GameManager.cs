using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    string sceneName = "SampleScene";
    public int lives = 3;
    int points = 0;

    public TextMeshPro savePoints;
    public LifesController livesController;
    JumperSpawner jumperSpawner;
    public GameObject gameOver;
    public GameObject input;


    void OnEnable()
    {
        JumperController.OnCrash += Lives;
        JumperController.OnSave += SavePoints;
    }

    void OnDisable()
    {
        JumperController.OnCrash -= Lives;
        JumperController.OnSave -= SavePoints;
    }

    void Start()
    {
        UpdatePointsLabel();
        livesController.InitLives(lives);
        jumperSpawner = GetComponent<JumperSpawner>();
        gameOver.SetActive(false);
    }

    public void Lives()
    {
        if (!livesController.RemoveLife())
        {
            GameOver();
        }
    }

    public void SavePoints()
    {
        points++;
        UpdatePointsLabel();
    }

    void UpdatePointsLabel()
    {
        savePoints.text = points.ToString();
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        jumperSpawner.Stop();
        input.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
