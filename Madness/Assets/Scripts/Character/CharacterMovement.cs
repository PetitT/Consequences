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
    public float groundCancelTime;

    [Header("JumpIncrease")]
    public float jumpIncTimer;
    public float jumpIncForce;
    private bool isIncreasingJump = false;

    [Header("Body")]
    public GameObject body;
    public Animator anim;

    [Header("Walls")]
    public LayerMask wallMask;
    public List<GameObject> wallChecks;
    public float wallCheckRange;

    private float currentJumpIncTimer;
    private float currentMoveSpeed;
    private float YMove;
    private bool isJumping = true;
    private bool isCancelingGround = false;

    private int currentTethers = 0;

    private Transform currentTarget;

    public AudioSource src;

    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
        currentJumpIncTimer = jumpIncTimer;

        EnemyTether.onStaticTetherStart += TetherStartHandler;
        EnemyTether.onStaticTetherEnd += TetherEndHandler;
    }

    private void OnDestroy()
    {
        EnemyTether.onStaticTetherStart -= TetherStartHandler;
        EnemyTether.onStaticTetherEnd -= TetherEndHandler;
    }

    private void TetherStartHandler(Transform target)
    {
        if (!src.isPlaying)
        {
            src.Play();
        }
        currentTarget = target;
        currentTethers++;
        anim.SetTrigger("StartCast");
    }

    private void TetherEndHandler()
    {
        currentTethers--;
        if (currentTethers <= 0)
        {
            src.Stop();
            currentTethers = 0;
            anim.SetTrigger("StopCast");
        }
    }

    void Update()
    {
        IncreaseJump();
        ApplyGravity();
        CheckGround();
        CheckWalls();
    }


    public void Move(float X)
    {
        if (currentTethers == 0)
        {
            if (X > 0)
            {
                anim.SetBool("IsMoving", true);
                body.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (X < 0)
            {
                anim.SetBool("IsMoving", true);
                body.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }
        else
        {
            if (currentTarget != null)
            {
                if (currentTarget.position.x > transform.position.x)
                {
                    body.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    body.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }

        if (!CheckWalls()) { return; }
        gameObject.transform.Translate(Vector2.right * X * currentMoveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            anim.SetTrigger("MakeJump");
            StartCoroutine(JumpGroundCancel());
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

    private bool CheckWalls()
    {
        foreach (var check in wallChecks)
        {
            if (Physics2D.Raycast(check.transform.position, body.transform.right, wallCheckRange, wallMask))
            {
                return false;
            }
        }
        return true;
    }

    private void CheckGround()
    {
        if (isCancelingGround) { return; }
        if (isJumping)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, distanceFromGround, ground))
            {
                isJumping = false;
                anim.SetTrigger("HitGround");
                YMove = 0;
            }
        }
        if (!isJumping)
        {
            if (!Physics2D.OverlapCircle(groundCheck.position, distanceFromGround, ground))
            {
                anim.SetTrigger("MakeJump");
                isJumping = true;
            }
        }
    }

    private IEnumerator JumpGroundCancel()
    {
        isCancelingGround = true;
        yield return new WaitForSeconds(groundCancelTime);
        isCancelingGround = false;
    }
}

