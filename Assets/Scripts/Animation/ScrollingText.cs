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
        if (gameObject.activeSelf)
        {
            StartCoroutine(SlideToNum(num));
        }
    }

    private IEnumerator SlideToNum(int num)
    {
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
    }
}
