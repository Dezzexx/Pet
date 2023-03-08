using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioBehaviourMB : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    
    public void OffMusicVol()
    {
        SetMusicVol(-80f);
    }
    public void OnMusicVol()
    {
        SetMusicVol(0f);
    }
    public void OffSoundsVol()
    {
        SetSoundsVol(-80f);
    }
    public void OnSoundsVol()
    {
        SetSoundsVol(0f);
    }
    private void SetMusicVol(float vol)
    {
        _mixer.SetFloat("MusicVol", vol);
    }
    private void SetSoundsVol(float vol)
    {
        _mixer.SetFloat("SoundsVol", vol);
    }
}
