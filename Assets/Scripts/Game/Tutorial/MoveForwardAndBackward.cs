using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveForwardAndBackward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<RectTransform>().DOAnchorPosY(GetComponent<RectTransform>().anchoredPosition.y+10f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }


}
