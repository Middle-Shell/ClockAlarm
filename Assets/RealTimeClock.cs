using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using TMPro;
using UnityEngine;

public class RealTimeClock : MonoBehaviour
{
    private DateTime dt1;
    [SerializeField] private bool StartClock;

    [SerializeField] private TextMeshProUGUI digitalClockText;
    [Space]
    [SerializeField] private Transform arrowH;
    [SerializeField] private Transform arrowM;
    [SerializeField] private Transform arrowS;
    [Space]
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;
    [SerializeField] private int _seconds;

    void Start()
    {
        //dt1 = GetExactTime("https://time100.ru/");
        
        /*_hours = dt1.Hour;
        _minutes = dt1.Minute;
        _seconds = dt1.Second;*/

        if (StartClock)
        {
            StartCoroutine(UpdateTimeFromServer());

            StartCoroutine(SecondsTime());
        }
    }

    IEnumerator UpdateTimeFromServer()
    {
        dt1 = GetExactTime();

        _hours = dt1.Hour;
        _minutes = dt1.Minute;
        _seconds = dt1.Second;
        UpdateArrow(_hours, _minutes, _seconds);
        
        yield return 0;
    }

    IEnumerator HoursTime()
    {
        _hours = _hours % 24 + 1;
        StartCoroutine(UpdateTimeFromServer());
        yield return null;
    }

    IEnumerator MinutesTime()
    {
        if (_minutes % 60 == 0)
        {
            StartCoroutine(HoursTime());
            _minutes = 0;
        }

        _minutes = _minutes % 60 + 1;
        arrowM.rotation = Quaternion.Euler(0, 0, -(_minutes * 6));
        arrowH.rotation = Quaternion.Euler(0, 0, -((_minutes / 2) + _hours * 30));
        yield return null;
    }

    IEnumerator SecondsTime()
    {
        while (true)
        {
            arrowS.rotation = Quaternion.Euler(0, 0, -(_seconds * 6));
            yield return new WaitForSecondsRealtime(1f);
            
            _seconds = _seconds % 60 + 1;
            
            if (_seconds % 60 == 0)
            {
                StartCoroutine(MinutesTime());
                _seconds = 0;
            }

            digitalClockText.text = _hours + ":" + _minutes + ":" + _seconds;
        }
    }

    public void UpdateArrow(int H, int M, int S)
    {
        arrowM.rotation = Quaternion.Euler(0, 0, -(M * 6));
        arrowH.rotation = Quaternion.Euler(0, 0, -((M / 2) + H * 30));
        arrowS.rotation = Quaternion.Euler(0, 0, -(S * 6));
    }

    public void GetArrowAngle(out float H, out float M, out float S)
    {
        H = arrowH.rotation.eulerAngles.z;
        M = arrowH.rotation.eulerAngles.z;
        S = arrowH.rotation.eulerAngles.z;
    }

    public static DateTime GetExactTime()
    {
        string url = "https://time100.ru/";
        using (var client = new HttpClient())
        {
            try
            {
                var result = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                if (result.Headers.Date.HasValue)
                {
                    return result.Headers.Date.Value.DateTime.ToLocalTime();
                }
                else
                {
                    throw new Exception("Cannot retrieve time from server.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error fetching time: {ex.Message}");
                return DateTime.Now;
            }
        }
    }
}
