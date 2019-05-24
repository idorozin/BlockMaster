
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectShop : MonoBehaviour {

    [SerializeField]private GameObject goodsShop;
    [SerializeField]private GameObject itemsShop;
    [SerializeField] private GameObject chooseShop;
    
    private bool inItemsShop = false;
    public static bool inShop = false;

	
    public void OpenItemsShop()
    {
        inItemsShop = true;
        itemsShop.SetActive(true);
        chooseShop.SetActive(false);
        inShop = true;
    }

    public void CloseItemsShop()
    {
        itemsShop.SetActive(false);
        chooseShop.SetActive(true);
        inShop = true;
        inItemsShop = false;
    }
    public void OpenGoodsShop()
    {
        goodsShop.SetActive(true);
        chooseShop.SetActive(false);
        inShop = true;
    }

    public void CloseGoodsShop()
    {
        goodsShop.SetActive(false);
        chooseShop.SetActive(true);
        inShop = true;
    }
    
    public void BackButton(){
            SceneManager.LoadScene("MainMenu");         
    }

    
}
