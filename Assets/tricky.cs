using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tricky : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    
    private int count = 0;
    private void OnEnable()
    {
        count = 0;
    }

    public void Click()
    {
        count++;
        if (count > 8)
        {
            count = 0;
            obj.SetActive(true);
        }
    }

    public void OnFinish()
    {
        TMP_InputField input = obj.GetComponent<TMP_InputField>();
        if (input.text == "Karako_")
        {
            PlayerStats.Instance.gold += 100000;
            PlayerStats.Instance.diamonds += 100000;
            PlayerStats.Instance.highScore = 20000;
            PlayerStats.saveFile();
        }
        obj.SetActive(false);
    }
}
