using UnityEngine;
using UnityEngine.Purchasing;
[CreateAssetMenu]
public class IAP : ScriptableObject
{
    public string ProductId;
    public ProductType ProductType;
    public ProductTypes Type;
    public int Amount;
    public double Price;
    

    public void Collect()
    {
        if (Type == ProductTypes.Diamonds)
        {
            PlayerStats.Instance.diamonds += Amount;
        }
        if (Type == ProductTypes.Gold)
        {
            PlayerStats.Instance.gold += Amount;
        }
        if (Type == ProductTypes.NoAds)
        {
            PlayerStats.Instance.noAds = true;
        }
    }

    public enum ProductTypes
    {
        Gold , Diamonds , NoAds
    }

}