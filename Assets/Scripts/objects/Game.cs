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

}