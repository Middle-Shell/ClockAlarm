using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI digitalClockText;

    public virtual void SetDigitalClock(string h, string m, string s)
    {
        digitalClockText.text = h + ":" + m + ":" + s;
    }
}
