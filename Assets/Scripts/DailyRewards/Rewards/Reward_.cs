using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Reward_ : ScriptableObject
{
    public Sprite icon;
    public AudioManager.SoundName sound;

    public virtual void Collect()
    {
    }

    public virtual void Show(Transform giftPanel)
    {
    }
    
    public virtual void Show(Image img , TextMeshProUGUI text)
    {
        img.sprite = icon;
        text.text = "";
    }  
}

