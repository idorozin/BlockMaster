using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnAndOff : MonoBehaviour
{
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;


    private int i = 0;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    { 
        one.SetActive(i%2==0);
        two.SetActive(i%2!=0);
        while (true)
        {
            i++;
            yield return new WaitForSecondsRealtime(3f);
            one.SetActive(i%2==0);
            two.SetActive(i%2!=0);
        }
    }


}
