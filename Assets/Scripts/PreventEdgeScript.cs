using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreventEdgeScript : MonoBehaviour
{
    public GameObject ball;
    public LayerMask groundLayer;
    private bool isGrounded;
    private MeshCollider mc;
    [SerializeField]
    [Range(0.01f, 1.5f)]
    private float rayDistance = 0.1f;
    private GameObject childPlane, childTileDetector;
    // Start is called before the first frame update
    void Start()
    {
        if (groundLayer == 0)
            groundLayer = LayerMask.GetMask("Ground");
        if (ball == null)
            ball = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            if (child.name.ContainsInsensitive("plane"))
                childPlane = child;
            else if (child.name.ContainsInsensitive("detector"))
                childTileDetector = child;
        }

        mc = childPlane.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(childPlane.transform.position, Vector3.down, rayDistance, groundLayer);

        mc.enabled = isGrounded;
        this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, this.transform.position.z);

        childTileDetector.transform.position = new Vector3(this.transform.position.x, ball.transform.position.y - 0.3f, this.transform.position.z);
    }
}
