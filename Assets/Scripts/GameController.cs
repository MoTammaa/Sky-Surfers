using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Tile;

public class GameController : MonoBehaviour
{
    #region Static Variables
    public static GameController current;
    #endregion

    #region Instance Variables
    public List<GameObject> Tiles;
    public GameObject EmptyChunk;
    public GameObject ball;
    [Range(0.5f, 2.5f)]
    public float GameSpeedMultiplier = 1f;
    public Game game;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        game = new Game();
    }
    // Update is called once per frame
    void Update()
    {
    }

}
