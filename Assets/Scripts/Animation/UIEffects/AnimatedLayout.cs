using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatedLayout : MonoBehaviour
{

    [SerializeField]
    private Transform startPosition;
    [SerializeField] 
    private Transform canvas;

    [SerializeField]
    private float startY = 350;

    [SerializeField] private float spacing = 50;

    private Vector3 nextPosition;

    [SerializeField]
    private bool fadeOut;

    private void OnEnable()
    {
        StartCoroutine(AnimateChildsIn());
    }

    [ContextMenu("disable")]
    public void Ondisable()
    {
        if (fadeOut)
        {
            gameObject.SetActive(true);
            StartCoroutine(AnimateChildsOut());
        }
    }

    [SerializeField] private float inDuration;
    [SerializeField] private float inDelay;
    

    private IEnumerator AnimateChildsIn()
    {
        float startY = this.startY;
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                childs.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (var obj in childs)
        {
            obj.SetActive(true);
            float screenHeight = canvas.GetComponent<RectTransform>().rect.height/2;
            float sizeY = obj.GetComponent<RectTransform>().rect.height/2;
            obj.transform.localPosition = new Vector3(0f , -screenHeight-sizeY , 0f);
            obj.transform.DOLocalMoveY(startY ,inDuration).SetEase(Ease.OutBounce);
            startY -= spacing;
            yield return new WaitForSecondsRealtime(inDelay);
        }
    }

    [SerializeField]
    private Ease ease;

    [SerializeField] private float outDuration;
    [SerializeField] private float outDelay;
    private IEnumerator AnimateChildsOut()
    {
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childs.Add(child.gameObject);
            startY -= 200;
        }
        foreach (var obj in childs)
        {
            float screenHeight = canvas.GetComponent<RectTransform>().rect.height;
            float sizeY = obj.GetComponent<RectTransform>().rect.height/2;
            //obj.transform.localPosition = new Vector3(0f , screenHeight+sizeY , 0f);
            obj.transform.DOMoveY(screenHeight+sizeY+1f ,outDuration).SetEase(ease);
            //obj.SetActive(true);
            yield return new WaitForSecondsRealtime(outDelay);
        }

        yield return new WaitForSecondsRealtime(outDuration - 0.2f);
        transform.parent.gameObject.SetActive(false);
    }

}
