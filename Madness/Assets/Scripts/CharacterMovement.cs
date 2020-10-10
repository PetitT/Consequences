using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float baseMoveSpeed;

    [Header("Jump")]
    public float baseJumpForce;
    public float gravity;
    public Transform groundCheck;
    public LayerMask ground;
    public float distanceFromGround;

    [Header("JumpIncrease")]
    public float jumpIncTimer;
    public float jumpIncForce;
    private bool isIncreasingJump = false;

    [Header("Body")]
    public GameObject body;

    private float currentJumpIncTimer;
    private float currentMoveSpeed;
    private float YMove;
    private bool isJumping = true;

    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
        currentJumpIncTimer = jumpIncTimer;
    }

    void Update()
    {
        IncreaseJump();
        ApplyGravity();
        CheckGround();
    }


    public void Move(float X)
    {
        if (X > 0)
        {
            body.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (X < 0)
        {
            body.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        gameObject.transform.Translate(Vector2.right * X * currentMoveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            YMove = baseJumpForce;
            isJumping = true;
            isIncreasingJump = true;
        }
    }

    private void IncreaseJump()
    {
        if (isIncreasingJump)
        {
            currentJumpIncTimer -= Time.deltaTime;
            YMove += jumpIncForce * Time.deltaTime;

            if (currentJumpIncTimer <= 0)
            {
                currentJumpIncTimer = jumpIncTimer;
                isIncreasingJump = false;
            }
        }
    }

    public void StopJumpIncrease()
    {
        if (isIncreasingJump)
        {
            isIncreasingJump = false;
            currentJumpIncTimer = jumpIncTimer;
        }
    }

    private void ApplyGravity()
    {
        if (isJumping)
        {
            YMove -= gravity * Time.deltaTime;
            gameObject.transform.Translate(Vector2.up * YMove * Time.deltaTime);
        }
    }

    private void CheckGround()
    {
        if (isJumping)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, distanceFromGround, ground))
            {
                isJumping = false;
                YMove = 0;
            }
        }
        if (!isJumping)
        {
            if (!Physics2D.OverlapCircle(groundCheck.position, distanceFromGround, ground))
            {
                isJumping = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, distanceFromGround);
    }
}

