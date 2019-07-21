using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSound : MonoBehaviour
{

    public AudioManager.SoundName name;

    private void OnEnable()
    {
        AudioManager.Instance.PlaySound(name);
    }
}
