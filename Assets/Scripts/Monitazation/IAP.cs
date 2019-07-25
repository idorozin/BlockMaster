using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

[CreateAssetMenu]
public class IAP : ScriptableObject
{
    public string ProductId;
    public ProductType ProductType;
    public ProductTypes Type;
    public int Amount;
    public double Price;
    public Sprite Image;
    public Vector2 size;
    public Vector2 position;


    public virtual void Collect()
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

    public void Buy()
    {
        IAPManager.Instance.BuyProductById(ProductId);
    }

    public enum ProductTypes
    {
        Gold , Diamonds , NoAds , Package
    }

}