using TMPro;
using UnityEngine;
using UnityEngine.UI;

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