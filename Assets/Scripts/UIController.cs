using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject alarmPanelH;
    [SerializeField] private GameObject alarmPanelV;
    [Space] 
    [SerializeField] private Button delAlarmBtnH;
    [SerializeField] private Button delAlarmBtnV;
    
    public void Close()
    {
        alarmPanelH.SetActive(false);
        alarmPanelV.SetActive(false);
    }
    
    public void Open()
    {
        alarmPanelH.SetActive(true);
        alarmPanelV.SetActive(true);
    }

    public void SwitchDelButton(bool on)
    {
        delAlarmBtnH.interactable = on;
        delAlarmBtnV.interactable = on;
    }
}
