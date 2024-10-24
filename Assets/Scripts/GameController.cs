using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static List<GameObject> Tiles;
    public static GameObject EmptyChunk;
    [Range(0.1f, 1.5f)]
    public static float GameSpeedMultiplier = 1.5f;

    public List<GameObject> tiles;
    public GameObject emptyChunk;
    public GameObject ball;
    [Range(0.5f, 2.5f)]
    public float gameSpeedMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new List<GameObject>();
        EmptyChunk = emptyChunk;
        foreach (var tile in tiles)
        {
            Tiles.Add(tile);
        }

    }
    // Update is called once per frame
    void Update()
    {
        GameSpeedMultiplier = gameSpeedMultiplier;
    }

}
