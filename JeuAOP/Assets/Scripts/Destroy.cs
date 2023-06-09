using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    private bool ishere=false;
    public Animator animator;
    private double cooldown = 1.3;
    private bool anim = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        ishere = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        ishere = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ishere)
        {
            animator.SetBool("PlayerInAndSit", true);
            anim = true;
        }            
        if (anim)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                sceneLoad();
            }
        }
    }

    private void sceneLoad()
    {
        SceneManager.LoadScene("TransitionScene");
    }
}
