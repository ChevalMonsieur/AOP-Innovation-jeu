using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typewriter : MonoBehaviour
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
        for(int i=0;i<=originalText.Length;i++)
        {
            uiText.text=originalText.Substring(0,i);
            yield return new WaitForSeconds(delai);
        }
        for(int i = 0; i < 20; i++)
        {
            uiText.text = null;
            yield return new WaitForSeconds(0.1f);
            uiText.text = originalText;
            yield return new WaitForSeconds(0.2f);
            uiText.text = null;
            yield return new WaitForSeconds(0.05f);
            uiText.text = originalText;
            yield return new WaitForSeconds(1f);

        }

    }
}
