using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject newchunk = Instantiate(GameController.EmptyChunk,
                new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z + 15), Quaternion.identity) as GameObject;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    int randomidx = Random.Range(0, GameController.Tiles.Count);
                    if (i != 1) while (GameController.Tiles[randomidx].name.Contains("empty"))
                            randomidx = Random.Range(0, GameController.Tiles.Count);
                    print(randomidx);
                    GameObject newtile = Instantiate(GameController.Tiles[randomidx]);
                    newtile.transform.parent = newchunk.transform;
                    newtile.transform.localPosition = new Vector3((j-1) * 2, 0,(i-1) * 5);
                }
            }
        }
    }
}
