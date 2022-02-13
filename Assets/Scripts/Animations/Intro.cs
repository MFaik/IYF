using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI FadeInText;
    float m_fadeInAlpha;
    [SerializeField] TextMeshProUGUI RevealingText;
    float m_characterInterval = .12f; 
    [SerializeField] Image FadeOut;

    void Start() {
        m_fadeInAlpha = 0;
        FadeInText.alpha = m_fadeInAlpha;
        RevealingText.maxVisibleCharacters = 0;

        StartCoroutine(nameof(RevealText));
    }

    void Update() {  
        if(Input.anyKeyDown){
            if(RevealingText.maxVisibleCharacters >= RevealingText.text.Length){
                FadeOut.DOFade(1,.5f);
                Invoke(nameof(NextScene),.5f);
            } else {
                m_fadeInAlpha = 1;
                FadeInText.alpha = m_fadeInAlpha;
                RevealingText.maxVisibleCharacters = RevealingText.text.Length;
            }
        }
    }

    IEnumerator RevealText() {
        while(m_fadeInAlpha < 1){
            m_fadeInAlpha += .1f;
            FadeInText.alpha = m_fadeInAlpha;
            yield return new WaitForSeconds(.1f);
        }
        while(RevealingText.maxVisibleCharacters < RevealingText.text.Length){
            RevealingText.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(m_characterInterval);
        }
    }

    void NextScene() {
        SceneManager.LoadScene(2);
    }
}
