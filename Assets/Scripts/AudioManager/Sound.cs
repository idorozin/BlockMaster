using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound
{
     public AudioManager.SoundName name;
     public AudioClip clip;
     public bool loop;
     [HideInInspector]
     public AudioSource source;
}
