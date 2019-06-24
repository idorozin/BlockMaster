
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
