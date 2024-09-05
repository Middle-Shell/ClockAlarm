using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClockAlarm : DigitalClock
{
    [SerializeField] private TMP_InputField hourText;
    [SerializeField] private TMP_InputField minutesText;

    public override void SetDigitalClock(string h = "0", string m = "15", string s = "0")
    {
        hourText.text = h;
        minutesText.text = m;
        digitalClockText.text = h + ":" + m;
    }
}
