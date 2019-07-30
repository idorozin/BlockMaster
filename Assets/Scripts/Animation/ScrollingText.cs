using System.Collections;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float animationSpeed;

    private int initialNum=0;
    
    
    public void SetNum(int num)
    {
        gameObject.SetActive(true);
        this.num = num;
        if (!isRunning)
        {
            StartCoroutine(SlideToNum());
        }
    }    
    public void SetNum(int num , int initialNum)
    {
        this.initialNum = initialNum;
        this.num = num;
        gameObject.SetActive(true);
        text.text = initialNum.ToString();
        if(!isRunning)
            StartCoroutine(SlideToNum());
    }

    private bool isRunning;
    private int num;
    private IEnumerator SlideToNum()
    {
        isRunning = true;
        while (initialNum < num)
        {
            initialNum++;
            text.text = initialNum.ToString();
            yield return new WaitForSecondsRealtime(animationSpeed);
        }
        while (initialNum > num)
        {
            initialNum--;
            text.text = initialNum.ToString();
            yield return new WaitForSecondsRealtime(animationSpeed);
        }
        isRunning = false;
    }
}
