using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Coroutine currentLaneSwitchCoroutine;
    private int lane = 0;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool canJump = true; // Cooldown flag
    public float jumpCooldown = 0.4f;
    private float rayDistance;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Collider collider = GetComponent<Collider>();

        float playerHeight = collider.bounds.size.y;

        // Adjust your ray distance slightly above the player height
        rayDistance = playerHeight / 2 + 0.1f;

    }

    void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        if (Input.GetButtonDown("Jump"))
            if (isGrounded && canJump)
                Jump();
            else SoundManager.current.PlayInvalid();

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (lane < 1)
                ChangeLane(lane + 1);
            else SoundManager.current.PlayInvalid();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (lane > -1)
                ChangeLane(lane - 1);
            else SoundManager.current.PlayInvalid();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Tile.DoAction(collision.gameObject.name);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    IEnumerator SwitchLane(int targetLane)
    {
        // ynfreeze x position
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        this.lane = targetLane;
        float tarX = targetLane * 2; // Convert lane index to x position {-2, 0, 2}
        float startX = this.transform.position.x;
        float elpTime = 0f;
        float duration = 0.18f;

        while (Math.Abs(this.transform.position.x - tarX) > 0.01f)
        {
            elpTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, tarX, elpTime / duration);
            this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
            yield return null;
        }

        // adjust params for consistency
        this.transform.position = new Vector3(tarX, this.transform.position.y, this.transform.position.z);
        // freeze x position using constraints
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

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
            // this.transform.position = new Vector3(this.lane * 2, this.transform.position.y, this.transform.position.z);
        }
        currentLaneSwitchCoroutine = StartCoroutine(SwitchLane(targetLane));
    }
}
