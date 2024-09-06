using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class RealTimeClock : MonoBehaviour
{
    private DateTime dt1;
    [SerializeField] private bool StartClock;

    [SerializeField] private DigitalClock digitalClock;
    [Space]
    [SerializeField] private Transform arrowH;
    [SerializeField] private Transform arrowM;
    [SerializeField] private Transform arrowS;
    [Space]
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;
    [SerializeField] private int _seconds;

    private async void OnEnable()
    {
        if (StartClock)
        {
            await UpdateTimeFromServer(); 
            StartCoroutine(SecondsTime());
        }
    }
    
    private async void OnApplicationFocus(bool hasFocus)
    {
        if (StartClock)
        {
            await UpdateTimeFromServer(); 
        }
    }

    private async Task UpdateTimeFromServer()
    {
        dt1 = await TimeFetcher.GetExactTime();

        _hours = dt1.Hour;
        _minutes = dt1.Minute;
        _seconds = dt1.Second;
        UpdateArrow(_hours, _minutes, _seconds);
        digitalClock.SetDigitalClock(_hours,_minutes, _seconds);
    }

    private IEnumerator SecondsTime()//работает ежесекундно раз в минуту запускает прибавление минуты
    {
        while (true)
        {
            arrowS.rotation = Quaternion.Euler(0, 0, -(_seconds * 6));
            yield return new WaitForSecondsRealtime(1f);

            _seconds = (_seconds + 1) % 60;
            if (_seconds == 0)
            {
                StartCoroutine(MinutesTime());
            }

            digitalClock.SetDigitalClock(_hours,_minutes, _seconds);
        }
    }

    private IEnumerator MinutesTime()//раз в час увеличвает час
    {
        _minutes = (_minutes + 1) % 60;
        if (_minutes == 0)
        {
            StartCoroutine(HoursTime());
        }

        arrowM.rotation = Quaternion.Euler(0, 0, -(_minutes * 6));
        arrowH.rotation = Quaternion.Euler(0, 0, -((_minutes / 2) + _hours * 30));
        yield return null;
    }

    private IEnumerator HoursTime()
    {
        _hours = (_hours + 1) % 24;
        arrowH.rotation = Quaternion.Euler(0, 0, -((_hours % 12 * 30) + (_minutes / 2)));
        UpdateTimeFromServer();//сверяется с интрнет часами
        yield return null;
    }

    public void UpdateArrow(int H, int M, int S)
    {
        arrowM.rotation = Quaternion.Euler(0, 0, -(M * 6));
        arrowH.rotation = Quaternion.Euler(0, 0, -((H % 12 * 30) + (M / 2)));
        arrowS.rotation = Quaternion.Euler(0, 0, -(S * 6));
    }

    public void GetArrowAngle(out float H, out float M, out float S)
    {
        H = arrowH.rotation.eulerAngles.z;
        M = arrowM.rotation.eulerAngles.z;
        S = arrowS.rotation.eulerAngles.z;
    }
}
