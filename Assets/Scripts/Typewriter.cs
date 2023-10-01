using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
    [SerializeField]
    private float speed = 50;

    // Update is called once per frame
    public Coroutine Run(string text, TMP_Text label){
        return StartCoroutine(TypeText(text, label));
    }

    private IEnumerator TypeText(string text, TMP_Text label){
        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length){
            t += Time.deltaTime * speed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            label.text = text.Substring(0, charIndex);

            yield return null;
        }

        label.text = text;
    }
}
