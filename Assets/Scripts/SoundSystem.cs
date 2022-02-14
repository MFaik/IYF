using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public AudioMixer m_audioMixer;

    bool muted = false;

    public void SetVolume() {
        if (muted){
            muted = false;
            m_audioMixer.SetFloat("Volume", 0f);
        }
        else{
            muted = true;
            m_audioMixer.SetFloat("Volume", -80f);
        }
    }
}
