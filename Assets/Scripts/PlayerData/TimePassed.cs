
using System;
using UnityEngine;

public class TimePassed 
{
    public DateTime startTime;
    public int offset=0;

    public TimePassed()
    {
        startTime = new DateTime();
        offset = 0;
    }
    
}