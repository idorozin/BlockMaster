using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(menuName = "Rewards/Random")]
public class RandomReward : Reward_
{
    public Countable[] rewards;
    public GameObject prefab;
    
    private int[] gamounts= {100,150,200,500};
    private int[] damounts= {1,2,3,5};

    public override void Collect()
    {
        reward.Collect();
    }

    public override void Show(Transform giftPanel)
    {
        GameObject go = Instantiate(prefab, giftPanel);
        go.GetComponentInChildren<Image>().sprite = icon;
    }

    private Countable reward;
    
    public override void Show(Image img , TextMeshProUGUI text)
    {
        reward = rewards[Random.Range(0, 1)];
        if(reward.GetType() == typeof(Diamonds))
            reward.amount = damounts[Random.Range(0,gamounts.Length-1)];
        else
            reward.amount = gamounts[Random.Range(0,gamounts.Length-1)];
        reward.Show(img,text);
    }  
}