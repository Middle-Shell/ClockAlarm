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
    
    [Space] 
    [SerializeField] private DigitalClockAlarm digitalClockAlarm;

    [SerializeField] private GameObject HArrow;
    [SerializeField] private GameObject MArrow;

    [SerializeField] private Toggle togglePM;

    private bool _validTime = false;

    public void CheckValidDataDigitalTime()
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
        digitalClockAlarm.SetDigitalClock();
    }

    public void ChangeArrow()
    {
        digitalClockAlarm.SetDigitalClock((11+(Int32)(-(HArrow.transform.rotation.eulerAngles.z / 30)) + (togglePM.isOn?12:0)).ToString(), (60 + (Int32)(-MArrow.transform.rotation.eulerAngles.z /6)).ToString());
    }

    public void SetAlarm()
    {
        notificationController.CancelNotification();
        //SetDigitalClock();

        UIController.SwitchDelButton(true);
        
        if (!_validTime)
        {
            print("Invalid time set.");
            return;
        }

        DateTime nowData = RealTimeClock.GetExactTime();

        int hours = int.Parse(hourText.text);
        int minutes = int.Parse(minutesText.text);
        

        DateTime alarmTime = new DateTime(nowData.Year, nowData.Month, nowData.Day, hours, minutes, 0);

        if (alarmTime <= nowData)
        {
            alarmTime = alarmTime.AddDays(1);
        }
        
        
        digitalClockAlarm.SetDigitalClock(hours.ToString(), minutes.ToString());
        
        notificationController.CreateNotification("ALARM!", "WAKE UP, SAMURAI", alarmTime);
    }

    
}
