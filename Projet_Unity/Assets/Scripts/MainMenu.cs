using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void Quitb()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void PlayB()
    {
        SceneManager.LoadScene("LoreBegin");
    }
}
