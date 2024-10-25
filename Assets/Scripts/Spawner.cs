using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject newchunk = Instantiate(GameController.current.EmptyChunk,
                new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z + 15), Quaternion.identity) as GameObject;
            //newchunk.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -8);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int randomidx = Random.Range(0, GameController.current.Tiles.Count);
                    if (i != 1) while (GameController.current.Tiles[randomidx].name.Contains("empty"))
                            randomidx = Random.Range(0, GameController.current.Tiles.Count);
                    GameObject newtile = Instantiate(GameController.current.Tiles[randomidx]);
                    newtile.transform.parent = newchunk.transform;
                    newtile.transform.localPosition = new Vector3((j - 1) * 2, 0, (i - 1) * 5);
                }
            }
        }
    }
}
