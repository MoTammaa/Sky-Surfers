using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Destroy(transform.parent.gameObject);
    }
}
