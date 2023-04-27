using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class ChangementScene : MonoBehaviour
{
    [SerializeField]
    //private int maxtime = 0;
    private GameObject cameraa;
    private Vector3 positionPreMove = Vector3.zero;
    private Vector3 reference = Vector3.zero;
    private Vector3 deplacement = Vector3.zero;
    public bool vertical;
    private bool move = false;
    public static int[] infos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        positionPreMove = new Vector3(cameraa.transform.position.x, cameraa.transform.position.y, cameraa.transform.position.z);
        if(collision.CompareTag("Player")) {
            move=true;
            if (vertical)
            {
                Debug.Log("vertical");
                if (Player.speedVerti > 0)
                {
                    deplacement = new Vector3(cameraa.transform.position.x, cameraa.transform.position.y + 14, cameraa.transform.position.z);
                }
                else if (Player.speedVerti < 0)
                {
                    deplacement = new Vector3(cameraa.transform.position.x, cameraa.transform.position.y - 14, cameraa.transform.position.z);
                }
            }
            else
            {
                if (Player.facingRight)
                {
                    deplacement = new Vector3(cameraa.transform.position.x + 25, cameraa.transform.position.y, cameraa.transform.position.z);
                }
                else if (!Player.facingRight)
                {
                    deplacement = new Vector3(cameraa.transform.position.x - 25, cameraa.transform.position.y, cameraa.transform.position.z);
                }
            }
        } 
    }

    private void Update()
    {
        MoveCheck();
    }

    private void MoveCheck() {
        if (move) {
            cameraa.transform.position = Vector3.SmoothDamp(cameraa.transform.position, deplacement, ref reference, 0.1f);
            if (Math.Abs(cameraa.transform.position.x-positionPreMove.x)>=24.99 || Math.Abs(cameraa.transform.position.y-positionPreMove.y)>=13.99) {
                move=false;
            cameraa.transform.position = Vector3.Lerp(cameraa.transform.position, deplacement, 0.2f);
            }
        }
    }
}

