using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private int tempsDash=-1;
    private float Hori;
    private float Verti;
    private bool facingRight = true;
    private bool isDoubleJumping;
    private Vector2 velocity = Vector2.zero;
    private bool jumping = false;
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

    void CheckSaut()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
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
            if (Input.GetMouseButtonDown(0) && dash == 2)
            {
                Hori = Input.GetAxisRaw("Horizontal");
                Verti = Input.GetAxisRaw("Vertical");
                tempsDash = 120;
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

/*    public static void AjouterForce(Vector2 vector)
    {
        (vector);
    }*/

    void UpdateAnimation()
    {
        animator.SetBool("jumping", jumping);
        animator.SetBool("isGrounded", isGrounded);
        //animator.SetInteger("Dash", dash);
        animator.SetFloat("speed", Math.Abs(rb.velocity.x));
    }
}
