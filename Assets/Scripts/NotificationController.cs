using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class NotificationController : MonoBehaviour
{
    private void Start()
    {
        RequestAuthorization();
        RegisterNotifications();
    }

    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }
    
    public void RegisterNotifications()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = "default_channel",
            Name = "Alarm",
            Importance = Importance.Default,
            Description = "WAKE UP, SAMURAI"
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string text, int H, int M)
    {
        var notification = new AndroidNotification();
        notification.Title = "ALARM";
        notification.Text = text;
        notification.FireTime = GetNotificationTime(H, M);
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    public void CancelNotification()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
    
    private DateTime GetNotificationTime(int hour, int minute)
    {
        // Get the current time
        var now = DateTime.Now;

        // Create a new DateTime with the specified hour and minute
        var notificationTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);

        // If the time has already passed today, schedule for tomorrow
        if (notificationTime <= now)
        {
            notificationTime = notificationTime.AddDays(1);
        }

        return notificationTime;
    }
    
}


/*using System;                    //сперва сделал систему для установки настоящего будильника посредством системного приложения, но оказалось, что android не предоставляет API для удаления т.к. не фиксирует
                                    какое приложение вызвало установку будильника. Альтернативный способ AlarmManager с возможностью удаления, у меня не заработал т.к. вызывал множество ошибок с Gradle при сборке билда
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    
    const string ACTION_SET_ALARM = "android.intent.action.SET_ALARM";
    const string EXTRA_HOUR = "android.intent.extra.alarm.HOUR";
    const string EXTRA_MINUTES = "android.intent.extra.alarm.MINUTES";
    const string EXTRA_MESSAGE = "android.intent.extra.alarm.MESSAGE";

    public void CreateAlarm(string message, int hour, int minutes)
    {
        var intentAJO = new AndroidJavaObject("android.content.Intent", ACTION_SET_ALARM);
        intentAJO
            .Call<AndroidJavaObject>("putExtra", EXTRA_MESSAGE, message)
            .Call<AndroidJavaObject>("putExtra", EXTRA_HOUR, hour)
            .Call<AndroidJavaObject>("putExtra", EXTRA_MINUTES, minutes);
		
        GetUnityActivity().Call("startActivity", intentAJO);
    }

    AndroidJavaObject GetUnityActivity()
    {
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}*/