using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI digitalClockText;

    public virtual void SetDigitalClock(int h, int m, int s)
    {
        digitalClockText.text = $"{h:D2}:{m:D2}:{s:D2}";
    }
}
