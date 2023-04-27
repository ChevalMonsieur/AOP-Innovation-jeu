using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typewriter2 : MonoBehaviour
{
    string originalText;
    Text uiText;
    public float delai = 0.2f;

    void Awake()
    {
        uiText = GetComponent<Text>();
        originalText = uiText.text;
        uiText.text = null;
        StartCoroutine(ShowLetterByLetter());
    }

    IEnumerator ShowLetterByLetter()
    {
        for (int i = 0; i <= originalText.Length; i++)
        {
            uiText.text = originalText.Substring(0, i);
            yield return new WaitForSeconds(delai);
        }

    }
}
