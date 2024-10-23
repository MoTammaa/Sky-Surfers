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

    public List<GameObject> tiles;
    public GameObject emptyChunk;
    public GameObject ball;
    private Coroutine currentLaneSwitchCoroutine;
    int lane = 0;

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

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && lane < 1)
        {
            ChangeLane(lane + 1);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && lane > -1)
        {
            ChangeLane(lane - 1);
        }

    }
    IEnumerator SwitchLane(int targetLane)
    {
        this.lane = targetLane;
        float targetX = targetLane * 2; // Convert lane index to x position {-2, 0, 2}
        float startX = ball.transform.position.x;
        float elapsedTime = 0f;
        float duration = 0.18f; // Adjust for desired speed

        while (Math.Abs(ball.transform.position.x - targetX) > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
            ball.transform.position = new Vector3(newX, ball.transform.position.y, ball.transform.position.z);
            yield return null; // Wait for the next frame
        }

        // Snap to exact position at the end
        ball.transform.position = new Vector3(targetX, ball.transform.position.y, ball.transform.position.z);
        StopCoroutine(currentLaneSwitchCoroutine);
        currentLaneSwitchCoroutine = null;
    }

    public void ChangeLane(int targetLane)
    {
        if (currentLaneSwitchCoroutine != null)
        {
            return;
            // StopCoroutine(currentLaneSwitchCoroutine);
            // // Tranlate the ball to the target lane and update the current lane
            // ball.transform.position = new Vector3(this.lane * 2, ball.transform.position.y, ball.transform.position.z);
        }
        currentLaneSwitchCoroutine = StartCoroutine(SwitchLane(targetLane));
    }


}
