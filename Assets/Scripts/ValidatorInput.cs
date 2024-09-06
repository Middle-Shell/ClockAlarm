using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidatorInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField textTMP;
    [SerializeField] private DigitalClockAlarm digitalClockAlarm;
    [SerializeField] private RealTimeClock clock;
    [SerializeField] private bool H = false;

    public void CheckValidDataDigitalTime()//проверка что пользователь ввёл верные числа, а не несуществующее время
    {
        if (H)
        {
            if (Convert.ToInt32(textTMP.text) >= 0 && Convert.ToInt32(textTMP.text) <= 23 || textTMP.text == "0")
            {

                clock.UpdateArrow(Convert.ToInt32(textTMP.text), Convert.ToInt32(textTMP.text), 0);
                return;
            }
        }
        else
        {
            if (Convert.ToInt32(textTMP.text) >= 0 && Convert.ToInt32(textTMP.text) <= 59 ||
                textTMP.text == "0")
            {
                clock.UpdateArrow(Convert.ToInt32(textTMP.text), Convert.ToInt32(textTMP.text), 0);
                return;
            }
        }

        print("ERROR");
        digitalClockAlarm.SetDigitalClock();
    }
}
