
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countable : Reward_
{
    public int amount;
    public Material material;
    public GameObject prefab;
 
    public override void Show(Transform giftPanel)
    {
        GameObject go = Instantiate(prefab, giftPanel);
        go.GetComponentInChildren<Image>().sprite = icon;
        TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();
        text.text = amount + "";
        text.fontMaterial = material;
    }    
    
    public override void Show(Image img , TextMeshProUGUI text)
    {
        img.sprite = icon;
        text.text = amount + "";
        text.fontMaterial = material;
    }
}
