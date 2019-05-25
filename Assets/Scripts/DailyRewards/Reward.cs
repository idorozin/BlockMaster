using UnityEngine;

[CreateAssetMenu]
public class Reward : ScriptableObject
{
    public int gold;
    public int diamonds;
    public bool cannon;
    public Sprite icon;

    public void Collect()
    {
        PlayerStats.Instance.gold += gold;
        PlayerStats.Instance.diamonds += diamonds;
    }
}

