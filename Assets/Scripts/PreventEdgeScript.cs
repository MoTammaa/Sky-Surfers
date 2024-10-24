using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        if (groundLayer == 0)
            groundLayer = LayerMask.GetMask("Ground");
        if (ball == null)
            ball = GameObject.FindGameObjectWithTag("Player");
        mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        mc.enabled = isGrounded;
        this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
