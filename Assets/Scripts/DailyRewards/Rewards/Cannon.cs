using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MyAsset;
[CreateAssetMenuAttribute(menuName = "Rewards/Cannon")]
public class Cannon : Reward_
{
    public int maxPrice;
    public GameObject prefab;
    private TextMeshProUGUI text;
    
    public override void Collect()
    {
        PlayerStats.Instance.ItemsOwned.Add(item.Id);
    }

    public override void Show(Transform giftPanel)
    {
        GameObject go = Instantiate(prefab, giftPanel);
        go.GetComponentInChildren<Image>().sprite = icon;
    }

    private Item item;
    
    public override void Show(Image img , TextMeshProUGUI text)
    {
        item = AssetDatabase.Instance.GetRandomCannon();   
        img.sprite = item.Icon;
        text.text = "";
    }  
}