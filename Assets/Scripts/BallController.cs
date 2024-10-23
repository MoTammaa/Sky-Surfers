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
    public float jumpCooldown = 0.2f;
    private float rayDistance;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Collider collider = GetComponent<Collider>();

        float playerHeight = collider.bounds.size.y;

        // Adjust your ray distance slightly above the player height
        rayDistance = playerHeight / 2 + 0.1f;

    }

    IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            Jump();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && lane < 1)
        {
            ChangeLane(lane + 1);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && lane > -1)
        {
            ChangeLane(lane - 1);
        }

    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(JumpCooldown());
    }

    IEnumerator SwitchLane(int targetLane)
    {
        this.lane = targetLane;
        float targetX = targetLane * 2; // Convert lane index to x position {-2, 0, 2}
        float startX = this.transform.position.x;
        float elapsedTime = 0f;
        float duration = 0.18f;

        while (Math.Abs(this.transform.position.x - targetX) > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
            this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
            yield return null;
        }

        // adjust params for consistency
        this.transform.position = new Vector3(targetX, this.transform.position.y, this.transform.position.z);
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
