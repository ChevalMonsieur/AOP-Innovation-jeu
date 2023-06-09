using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToGame : MonoBehaviour
{
    public void begin()
    {
        SceneManager.LoadScene("TutoRoom");
    }
}
