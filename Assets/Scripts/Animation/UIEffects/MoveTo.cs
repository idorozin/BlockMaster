using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class MoveTo : Effect
{
    [SerializeField]
    private Vector3 finalPos;
    [SerializeField] 
    private Vector3 startPos;
    [SerializeField] 
    private Ease ease;

    protected override IEnumerator Animate()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = startPos;
        yield return new WaitForSecondsRealtime(delay);
        rect.DOAnchorPos(finalPos  , duration).SetEase(ease);
    }
}
