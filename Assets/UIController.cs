using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject alarmPanel;
    [Space] 
    [SerializeField] private Button delAlarmBtn;
    
    public void Close()
    {
        alarmPanel.SetActive(false);
    }
    
    public void Open()
    {
        alarmPanel.SetActive(true);
    }

    public void SwitchDelButton(bool on)
    {
        delAlarmBtn.interactable = on;
    }
}
