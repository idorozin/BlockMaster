using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Reward_ : ScriptableObject
{
    public Sprite icon;

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


public class Countable : Reward_
{
    public int amount;
    public Color32 faceColor;
    public Color32 outlineColor;
    public GameObject prefab;

    protected virtual void OnEnable()
    {
        Debug.Log("load prefab");
        prefab = Resources.Load<GameObject>("Gifts/Gift");
    }
    
    public override void Show(Transform giftPanel)
    {
        GameObject go = Instantiate(prefab, giftPanel);
        go.GetComponentInChildren<Image>().sprite = icon;
        TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();
        text.text = amount + "";
        text.faceColor = faceColor;
        text.outlineColor = outlineColor;
    }    
    
    public override void Show(Image img , TextMeshProUGUI text)
    {
        img.sprite = icon;
        text.text = amount + "";
        text.faceColor = faceColor;
        text.outlineColor = outlineColor;
    }
}


[CreateAssetMenuAttribute(menuName = "Rewards/Gold")]
public class Gold : Countable
{
    protected override void OnEnable()
    {
        base.OnEnable();
        icon = Resources.Load<Sprite>("CoinsLogo");
        faceColor = new Color32(255 , 238 ,0 ,255);
        outlineColor = new Color32(255 , 181 ,0 ,255);
    }

    public override void Collect()
    {
        PlayerStats.Instance.gold += amount;
    }
}

[CreateAssetMenuAttribute(menuName = "Rewards/Diamonds")]
public class Diamonds : Countable
{    
    private void OnEnable()
    {
        base.OnEnable();
        icon = Resources.Load<Sprite>("DiamondLogo");
        faceColor = new Color32(0 , 241 ,255 ,255);
        outlineColor = new Color32(0 , 139 ,255 ,255);
    }

    public override void Collect()
    {
        PlayerStats.Instance.diamonds += amount;
    }
}

[CreateAssetMenuAttribute(menuName = "Rewards/Cannon")]
public class Cannon : Reward_
{
    public new string name;
    private GameObject prefab;
    
    private void OnEnable()
    {
        prefab = (GameObject)Resources.Load("Gifts/GiftNoText");
    }
    
    public override void Collect()
    {
        PlayerStats.Instance.ItemsOwned.Add(13);
    }

    public override void Show(Transform giftPanel)
    {
        GameObject go = Instantiate(prefab, giftPanel);
        go.GetComponentInChildren<Image>().sprite = icon;
    }
    
    public override void Show(Image img , TextMeshProUGUI text)
    {
        img.sprite = icon;
        text.text = "";
    }  
}