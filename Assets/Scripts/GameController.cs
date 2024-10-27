using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Tile;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Static Variables
    public static GameController current;
    public static bool SoundEnabled { get { return SoundManager.SoundEnabled; } set { SoundManager.SoundEnabled = value; } }
    #endregion

    #region Instance Variables
    public List<GameObject> Tiles;
    public GameObject EmptyChunk;
    public GameObject ball;
    // [Range(0.5f, 2.5f)]
    // public float GameSpeedMultiplier = 1f;
    public Game game;
    public TMPro.TextMeshProUGUI scoreFuelText, speedText;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        game = new Game();
        game.StartGame();
    }
    // Update is called once per frame
    void Update()
    {
        // game.GameSpeedMultiplier = GameSpeedMultiplier;
        if (game.CurrentState == Game.GameState.GameOver)
        {
            Debug.Log("Game Over");
            SoundManager.current.PlayGameOver();
            Time.timeScale = 0;
        }

        if (game.CurrentState == Game.GameState.Playing)
        {
            UpdateFuelnScore();
            UpdateText_ScoreFuelSpeed();
        }
    }

    void UpdateFuelnScore()
    {
        game.Score += Time.deltaTime;
        game.fuel -= game.fuelRate * Time.deltaTime;
        if (game.fuel <= 0)
        {
            game.CurrentState = Game.GameState.GameOver;
        }
    }

    void UpdateText_ScoreFuelSpeed()
    {
        scoreFuelText.text = "Score: " + Math.Round(game.Score, 0) + "\nFuel: " + Math.Round(game.fuel, 0);
        speedText.text = "Speed: " + (game.GameSpeedMultiplier == 1f ? "Normal" : "High");
    }


    public IEnumerator StartGameMusic()
    {
        while (SoundManager.current == null)
            yield return new WaitForSeconds(0.5f);
        SoundManager.current.PlayGameTrack();
    }
}
