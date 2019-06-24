using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : Countable
{
    public string ProductId;
    public ProductType ProductType;

    public override void Collect()
    {
        PlayerStats.Instance.gold += amount;
    }

}