using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class ChangementScene : MonoBehaviour
{
    public int SceneArrivee;
    public static int[] infos;
    public void Start()
    {
        
    }

    public static int Getinfo(int info)
    {
        return infos[info];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadAndSaveData.instance.SaveData();
        SceneManager.LoadScene(SceneArrivee);
        //Mouvement.AjouterForce(Arrivee[NumeroScene]);
    }
}
