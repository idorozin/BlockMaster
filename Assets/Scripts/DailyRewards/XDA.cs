using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XDA : DailyReward_
{
    [SerializeField]
    private Button button;
    [SerializeField] 
    private TextMeshProUGUI text;    
    
    protected override void SetTimePassed()
    {
        if(PlayerStats.Instance.test == null)
            PlayerStats.Instance.test = new TimePassed();
        timePassed = PlayerStats.Instance.test;
    }

    protected override bool OnValidation()
    {
        Debug.Log("validation");
        return false;
    }

    protected override void OnTimePassed()
    {
        button.enabled = true;
    }

    protected override void OnTick()
    {
        text.text = ExtensionMethods.SecsToTime(coolDown);
    }
    
    protected override void OnReset()
    {
        
    }

    public void Click()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
            return;
        //if(validationRequired)      
        StartCoroutine(ResetTimer());
    }
}
