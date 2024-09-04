using System;
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmController : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    [SerializeField] private RealTimeClock clock;
    [Space]
    [SerializeField] private TMP_InputField hourText;
    [SerializeField] private TMP_InputField minutesText;

    private bool validTime = false;

    private void Start()
    {
        InitializeNotifications();
    }

    private void InitializeNotifications()
    {
        GameNotificationChannel channel = new GameNotificationChannel("Alarm", "Alarm", "Alarm");
        notificationsManager.Initialize(channel);
    }

    public void CheckValidDataDigitalTime()
    {
        if (Convert.ToInt32(hourText.text) >= 0 && Convert.ToInt32(hourText.text) <= 23 || hourText.text == "0")
        {
            if (Convert.ToInt32(minutesText.text) >= 0 && Convert.ToInt32(minutesText.text) <= 59  || minutesText.text == "0")
            {
                clock.UpdateArrow(Convert.ToInt32(hourText.text), Convert.ToInt32(minutesText.text), 0);
                validTime = true;
                return;
            }
        }
        print("ERROR");
        hourText.text = "0";
        minutesText.text = "0";
    }

    public void SetAlarm()
    {
        if (!validTime)
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

        CreateNotification("ALARM!", "WAKE UP, SAMURAI", alarmTime);
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationsManager.CreateNotification();
        if (notification != null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;

            notificationsManager.ScheduleNotification(notification);
        }
    }
}
