using System;
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    
    public void CancelNotification()
    {
        notificationsManager.CancelAllNotifications();
        //alarmDigitalText.text = "--" + ":" + "--";
        //delAlarmBtn.interactable = false;
    }
    
    public void CreateNotification(string title, string body, DateTime time)
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
    
    private void InitializeNotifications()
    {
        GameNotificationChannel channel = new GameNotificationChannel("Alarm", "Alarm", "Alarm");
        notificationsManager.Initialize(channel);
    }
    
    private void Start()
    {
        InitializeNotifications();
    }
}
