using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveIn : Effect
{
    [SerializeField]
    private float finalX = 400;
    
    protected override IEnumerator Animate()
    {
        yield return new WaitForSecondsRealtime(delay);
        transform.DOMoveX(finalX  , duration);
        yield break;
    }
}
