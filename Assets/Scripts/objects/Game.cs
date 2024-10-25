using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Starting,
    }

    public GameState CurrentState { get; set; } = GameState.Starting;

    public int Score = 0;
    public int fuelRate = 1;
    public float fuel = 50f;

    [Range(0.5f, 2.5f)]
    public float GameSpeedMultiplier = 1f;

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Score = 0;
        fuel = 50;
        fuelRate = 1;

        GameController.current.StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while (CurrentState == GameState.Playing)
        {
            yield return new WaitForSeconds(1);
            Score += 1;
        }

        yield return null;
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        Time.timeScale = 0;
        GameController.print("Game Over");
    }

}