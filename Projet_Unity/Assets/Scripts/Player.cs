using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour
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
    public static float speedVerti;
    public static float speedHori;
    private float tempsDash=-1;
    private float Hori;
    private float Verti;
    public static bool facingRight = true;
    private bool isDoubleJumping;
    public static Vector2 velocity = Vector2.zero;
    private bool jumping = false;
    private bool sitting = false;
    private void Update()
    {
        isGrounded = IsGrounded();
        if (!SitCheck()) {
            MovePlayerHori();
            FacingDirection();
            CheckSaut();
            isDoubleJumping = CheckDoubleSaut();
            CheckDash();
        }
        UpdateAnimation();
        speedVerti = rb.velocity.y;
        speedHori = rb.velocity.x;
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
            transform.localScale = new Vector3(Convert.ToSingle(-1), Convert.ToSingle(1), Convert.ToSingle(1));
            facingRight = true;
        }
        if (rb.velocity.x < -0.1)
        {
            transform.localScale = new Vector3(Convert.ToSingle(1), Convert.ToSingle(1), Convert.ToSingle(1));
            facingRight = false;
        }
    }

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, solLayer); 
    }

    bool SitCheck()
    {
        if (sitting) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sitting = false;
            }
        } else {
            if (rb.velocity.x<0.1 && rb.velocity.x > -0.1) {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rb.velocity = Vector3.zero;
                    sitting = true;
                }
            }
        }
        return sitting;
    }

    void CheckSaut()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumping= true;
                rb.AddForce(new Vector2(0, forceSaut));
            }
            dash = 2;
            doubleSaut = true;
        }
        else
        {
            jumping = false;
        }
    }

    bool CheckDoubleSaut()
    {
        bool res = false;
        if (doubleSaut)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !jumping)
            {
                jumping = true;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0, forceDoubleSaut));
                doubleSaut = false;
                res = true;
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
                tempsDash = 150;
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
                rb.velocity /= 3;
            }
        }
    }

/*    public static void AjouterForce(Vector2 vector)
    {
        (vector);
    }*/

    void UpdateAnimation()
    {
        animator.SetBool("isSit", sitting);
        animator.SetBool("jumping", jumping);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speedHori", Math.Abs(rb.velocity.x));
    }
}
