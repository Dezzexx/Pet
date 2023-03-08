using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InterfaceConfig", menuName = "Configs/InterfaceConfig", order = 0)]

public class InterfaceConfig : ScriptableObject
{
    public Sprite 
        music_on,
        music_off,
        sound_on,
        sound_off,
        vibro_on,
        vibro_off;
}
