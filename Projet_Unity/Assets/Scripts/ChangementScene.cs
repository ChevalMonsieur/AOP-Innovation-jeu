using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public int NumeroScene;
    public Vector2[] Arrivee;

    public void Start()
    {
        Arrivee[0] = new Vector2(100,200);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NumeroScene);
        //Mouvement.AjouterForce(Arrivee[NumeroScene]);
    }
}
