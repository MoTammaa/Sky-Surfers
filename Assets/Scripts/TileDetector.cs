using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetector : MonoBehaviour
{
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        if (groundLayer == 0)
            groundLayer = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player entered Detector");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((groundLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            print("Ground detected: " + other.gameObject.name);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player is in Detector");
        }
    }
}
