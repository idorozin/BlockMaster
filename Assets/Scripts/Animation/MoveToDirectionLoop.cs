using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveToDirectionLoop : MonoBehaviour
{
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 pos;

    private void OnEnable()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = start;
        GetComponent<RectTransform>().DOAnchorPos(pos,1.5f).SetEase(Ease.OutCubic);
    }

}
