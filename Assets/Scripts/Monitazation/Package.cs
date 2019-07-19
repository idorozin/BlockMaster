using UnityEngine;

[CreateAssetMenu]
public class Package : IAP
{
    [SerializeField]
    private IAP[] products;

    public override void Collect()
    {
        foreach (var product in products)
        {
            product.Collect();
        }
    }
}