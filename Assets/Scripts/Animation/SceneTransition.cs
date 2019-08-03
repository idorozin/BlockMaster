using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] 
    private Image image;
    [SerializeField] 
    private Color endColor;
    [SerializeField] 
    private Color startColor;
    [SerializeField] 
    private float duration;
    [SerializeField]
    private bool fadeIn = true;

    [SerializeField] private static bool fadeCompleted = true; 

    private bool running;
    // Start is called before the first frame update
    private void Start()
    {
        if(!fadeIn && fadeCompleted)
            return;
        image.gameObject.SetActive(true);
        image.color = startColor;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForEndOfFrame();
        //fade in
        //DOTween.timeScale = 1f;
        image.DOColor(endColor, duration).SetUpdate(true).OnComplete(
            ()=>
            {
                fadeCompleted = true;
                image.gameObject.SetActive(false);
            });
    }

    public void FadeOutAndLoadScene(string name)
    {
        image.color = endColor;
        image.gameObject.SetActive(true);
        image.DOColor(startColor, duration).SetUpdate(true).OnComplete(
            () => SceneManager.LoadScene(name));
        fadeCompleted = false;
    }
}