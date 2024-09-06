using System;
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmController : MonoBehaviour
{
    
    [SerializeField] private RealTimeClock clock;
    [Space]
    [SerializeField] private TMP_InputField hourText;
    [SerializeField] private TMP_InputField minutesText;

    [Space]
    [SerializeField] private NotificationController notificationController;

    [Space] 
    [SerializeField] private UIController UIController;
    
    [Space] //TODO переделать в ивент
    [SerializeField] private DigitalClockAlarm digitalClockAlarmV;
    [SerializeField] private DigitalClockAlarm digitalClockAlarmH;

    [SerializeField] private GameObject HArrow;
    [SerializeField] private GameObject MArrow;

    [SerializeField] private Toggle togglePM;

    private int _hSave;
    private int _mSave;

    /*public void CheckValidDataDigitalTime()
    {
        if (Convert.ToInt32(hourText.text) >= 0 && Convert.ToInt32(hourText.text) <= 23 || hourText.text == "0")
        {
            if (Convert.ToInt32(minutesText.text) >= 0 && Convert.ToInt32(minutesText.text) <= 59  || minutesText.text == "0")
            {
                clock.UpdateArrow(Convert.ToInt32(hourText.text), Convert.ToInt32(minutesText.text), 0);
                _validTime = true;
                return;
            }
        }
        print("ERROR");
        digitalClockAlarmV.SetDigitalClock();
        digitalClockAlarmH.SetDigitalClock();
    }*/

    public void ChangeArrow()
    {//время высчитывается исхлдя из угла стрелки, для часовой - Z/30 для минутной z/6 для часовой так же учитывается нужно ли нам время после 11 утра, что прибавляет + 12 часов
        digitalClockAlarmV.SetDigitalClock((11+(Int32)(-(HArrow.transform.rotation.eulerAngles.z / 30)) + (togglePM.isOn?12:0)), (60 + (Int32)(-MArrow.transform.rotation.eulerAngles.z /6)));
        digitalClockAlarmH.SetDigitalClock((11+(Int32)(-(HArrow.transform.rotation.eulerAngles.z / 30)) + (togglePM.isOn?12:0)), (60 + (Int32)(-MArrow.transform.rotation.eulerAngles.z /6)));
    }

    public void SetAlarm()
    {
        notificationController.CancelNotification();
        //SetDigitalClock();

        UIController.SwitchDelButton(true);

        int hours = int.Parse(hourText.text);
        int minutes = int.Parse(minutesText.text);
        
        digitalClockAlarmV.SetAlarmClock(hours, minutes);
        digitalClockAlarmH.SetAlarmClock(hours, minutes);
        
        notificationController.SendNotification("WAKE UP, SAMURAI", hours, minutes);
        
        //_hSave = hours;
        //_mSave = minutes;
    }

    
}
