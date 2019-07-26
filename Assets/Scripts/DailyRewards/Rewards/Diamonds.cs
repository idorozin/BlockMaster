using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/Diamonds")]
public class Diamonds : Countable
{    
    public override void Collect()
    {
        PlayerStats.Instance.diamonds += amount;
    }
}