using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveUpLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y+1.5f,1.5f).SetEase(Ease.OutCubic).SetLoops(-1,LoopType.Restart);
    }


}
