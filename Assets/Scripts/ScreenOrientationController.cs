using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationController : MonoBehaviour
{
    private ScreenOrientation lastOrientation;

    [SerializeField] private GameObject VerticalCanvas;
    [SerializeField] private GameObject HorizontalCanvas;

    void Start()
    {
        lastOrientation = Screen.orientation;
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            Debug.Log("Ориентация экрана изменена на: " + Screen.orientation);

            lastOrientation = Screen.orientation;

            OnOrientationChanged();
        }
    }

    void OnOrientationChanged()
    {
        if (Screen.orientation == ScreenOrientation.Portrait || 
            Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            VerticalCanvas.SetActive(true);
            HorizontalCanvas.SetActive(false);
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft || 
                 Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            VerticalCanvas.SetActive(false);
            HorizontalCanvas.SetActive(true);
        }
    }
}
