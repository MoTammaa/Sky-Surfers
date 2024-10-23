using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static List<GameObject> Tiles;

    [SerializeField]
    public List<GameObject> tiles;

    public static GameObject EmptyChunk;

    public GameObject emptyChunk;

    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
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
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, ball.transform.position.z - 5);
    }
}
