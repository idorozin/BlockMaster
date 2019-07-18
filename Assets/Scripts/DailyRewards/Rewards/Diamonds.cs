using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/Diamonds")]
public class Diamonds : Countable
{    
    private void OnEnable()
    {
        base.OnEnable();
        if (icon != null ) 
            return;
        icon = Resources.Load<Sprite>("DiamondLogo");
        faceColor = new Color32(0 , 241 ,255 ,255);
        outlineColor = new Color32(0 , 139 ,255 ,255);
    }

    public override void Collect()
    {
        PlayerStats.Instance.diamonds += amount;
    }
}