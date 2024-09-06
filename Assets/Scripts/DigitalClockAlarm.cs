using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClockAlarm : DigitalClock
{
    [SerializeField] private TMP_InputField hourText;
    [SerializeField] private TMP_InputField minutesText;

    public override void SetDigitalClock(int h = 0, int m = 15, int s = 0)
    {
        hourText.text = h.ToString();
        minutesText.text = m.ToString();
    }

    public void SetAlarmClock(int h, int m)
    {
        digitalClockText.text = $"{h:D2}:{m:D2}";
    }
}
