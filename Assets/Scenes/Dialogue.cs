using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public AudioSource soundEffect;
    void Start() {
        textComponent.text=string.Empty;
        StartCoroutine(TypeLine("this is a very long test of tests"));
    }
    IEnumerator TypeLine(String s){
        foreach(char c in s.ToCharArray()){
            textComponent.text+=c;
            if(Char.IsLetterOrDigit(c))
                soundEffect.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
