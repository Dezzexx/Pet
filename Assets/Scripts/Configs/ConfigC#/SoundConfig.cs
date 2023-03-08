using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundsConfig", menuName = "Configs/SoundsConfig", order = 2)]
public class SoundConfig : ScriptableObject
{
    public AudioClip LoseSound;
    public AudioClip WinSound;
    public AudioClip ClickSound;
    public AudioClip BuySound;
    public AudioClip FailClickSound;
}

