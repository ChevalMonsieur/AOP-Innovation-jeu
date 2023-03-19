using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mouvement : MonoBehaviour
{
    public Animator animator;
    public float groundCheckRadius;
    public LayerMask solLayer;
    public Transform groundCheck;
    public static bool isGrounded;
    public float vitesse;
    public float forceSaut;
    public float forceDoubleSaut;
    public Rigidbody2D rb;
    public bool doubleSaut;
    public int dash;

    private bool JumpStart;
    private int tempsDash=-1;
    private float Hori;
    private float Verti;
    private bool facingRight = true;
    private bool isDoubleJumping;
    private Vector2 velocity = Vector2.zero;

    private void Update()
    {
        MovePlayerHori();
        FacingDirection();
        isGrounded = IsGrounded();
        CheckSaut();
        isDoubleJumping = CheckDoubleSaut();
        CheckDash();
        UpdateAnimation();
    }

    void MovePlayerHori()
    {
        float MouvementHori = Input.GetAxis("Horizontal") * vitesse;
        Vector2 targetvelocity = new(MouvementHori, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(targetvelocity, rb.velocity, ref velocity, 0.05f);

    }
    void FacingDirection()
    {
        if (rb.velocity.x > 0.1)
        {
            transform.localScale = new Vector3(Convert.ToSingle(0.6), Convert.ToSingle(0.6), Convert.ToSingle(0.6));
            facingRight = true;
        }
        if (rb.velocity.x < -0.1)
        {
            transform.localScale = new Vector3(Convert.ToSingle(-0.6), Convert.ToSingle(0.6), Convert.ToSingle(0.6));
            facingRight = false;
        }
    }

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, solLayer); 
    }

    void CheckSaut()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(new Vector2(0, forceSaut));
            }
            dash = 2;
            doubleSaut = true;
        }
    }

    bool CheckDoubleSaut()
    {
        bool res = false;
        if (doubleSaut)
        {
            JumpStart = false;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0, forceDoubleSaut));
                doubleSaut = false;
                res = true;
                JumpStart = true;
            }
        }
        return res;
    }

    void CheckDash()
    {
        if (dash == 2 || dash == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && dash == 2)
            {
                Hori = Input.GetAxisRaw("Horizontal");
                Verti = Input.GetAxisRaw("Vertical");
                tempsDash = 20;
                dash = 1;
            }
            if (dash == 1)
            {
                rb.velocity = new Vector2(0, 0);
                if (Hori == 0 && Verti == 0)
                {
                    if (facingRight)
                    {
                        rb.velocity = new Vector2(15, 1);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-15, 1);
                    }
                }
                else
                {
                    if (Math.Abs(Hori) == 1 && Math.Abs(Verti) == 1)
                    {
                        rb.velocity = new Vector2(10 * Hori, 8 * Verti);
                    }
                    else
                    {
                        rb.velocity = new Vector2(15 * Hori, 8 * Verti);
                    }
                }
                tempsDash--;
            }
            if (tempsDash == 0)
            {
                dash = 0;
                tempsDash = -1;
                rb.velocity /= 2;
            }
        }
    }

    void UpdateAnimation()
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("DoubleJump", isDoubleJumping);
        animator.SetInteger("Dash", dash);
        animator.SetFloat("Hori", Math.Abs(Hori));
        animator.SetFloat("Verti", Verti);
        animator.SetFloat("Speed", Math.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", JumpStart);
    }
}
