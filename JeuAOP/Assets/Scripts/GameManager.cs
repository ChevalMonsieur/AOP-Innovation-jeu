using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    void Start()
    {

    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("PositionX", ChangementScene.infos[0]);
        PlayerPrefs.SetInt("PositionY", ChangementScene.infos[1]);
        PlayerPrefs.SetInt("ForceX", ChangementScene.infos[2]);
        PlayerPrefs.SetInt("ForceY", ChangementScene.infos[3]);
    }
}
