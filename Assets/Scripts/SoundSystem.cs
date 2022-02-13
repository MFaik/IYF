using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] Sprite PlaySprite;
    [SerializeField] Sprite MutedSprite;

    Image m_image;

    public AudioMixer m_audioMixer;

    bool muted = false;

    void Start() {
        m_image = GetComponent<Image>();
    }

    public void SetVolume() {
        if (muted){
            m_image.sprite = PlaySprite;
            muted = false;
            m_audioMixer.SetFloat("Volume", 0f);
        }
        else{
            m_image.sprite = MutedSprite;
            muted = true;
            m_audioMixer.SetFloat("Volume", -80f);
        }
    }
}
