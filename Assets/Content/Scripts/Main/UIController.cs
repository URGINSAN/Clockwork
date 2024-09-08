using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private Text ClockDigitalText;
    [SerializeField] private Transform HoursArrow;
    [SerializeField] private Transform MinutesArrow;
    [SerializeField] private Transform SecondsArrow;
    [Header("Test")]
    public int Hours;
    public int Minutes;
    public int Seconds;
    public bool Test;


    private const float
    hoursToDegrees = 360f / 12f,
    minutesToDegrees = 360f / 60f,
    secondsToDegrees = 360f / 60f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Test)
        {
            UpdateTime(Hours, Minutes, Seconds);
            Test = false;
        }
    }

    public void UpdateTime(int hours, int minutes, int seconds)
    {
        ClockDigitalText.text = $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";

        HoursArrow.localRotation = Quaternion.Euler(0f, 0f, (hours * -hoursToDegrees) + 180);
        MinutesArrow.localRotation = Quaternion.Euler(0f, 0f, (minutes * -minutesToDegrees) + 180);
        SecondsArrow.localRotation = Quaternion.Euler(0f, 0f, (seconds * -secondsToDegrees) + 180);
    }
}
