using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public InputField ClockDigitalHoursText;
    public InputField ClockDigitalMinutesText;
    public InputField ClockDigitalSecondsText;
    [Space]
    [SerializeField] private Transform HoursArrow;
    [SerializeField] private Transform MinutesArrow;
    [SerializeField] private Transform SecondsArrow;
    [Space]
    [SerializeField] private int Hours;
    [SerializeField] private int Minutes;
    [SerializeField] private int Seconds;


    private const float
    hoursToDegrees = 360f / 12f,
    minutesToDegrees = 360f / 60f,
    secondsToDegrees = 360f / 60f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        ClockDigitalHoursText.interactable = false;
        ClockDigitalMinutesText.interactable = false;
        ClockDigitalSecondsText.interactable = false;
    }

    public void UpdateTime(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;

        ClockDigitalHoursText.text = hours.ToString("00");
        ClockDigitalMinutesText.text = minutes.ToString("00");
        ClockDigitalSecondsText.text = seconds.ToString("00");

        HoursArrow.localRotation = Quaternion.Euler(0f, 0f, (hours * -hoursToDegrees));// - (minutes / 2));
        MinutesArrow.localRotation = Quaternion.Euler(0f, 0f, minutes * -minutesToDegrees);
        SecondsArrow.localRotation = Quaternion.Euler(0f, 0f, seconds * -secondsToDegrees);
    }

    public void AdjustAlarmArrows(int hours, int minutes, int seconds)
    {
        ClockDigitalHoursText.text = hours.ToString("00");
        ClockDigitalMinutesText.text = minutes.ToString("00");
        ClockDigitalSecondsText.text = seconds.ToString("00");

        HoursArrow.localRotation = Quaternion.Euler(0f, 0f, (hours * -hoursToDegrees));
        MinutesArrow.localRotation = Quaternion.Euler(0f, 0f, minutes * -minutesToDegrees);
        SecondsArrow.localRotation = Quaternion.Euler(0f, 0f, seconds * -secondsToDegrees);
    }

    public int ArrowTime(int index, float rot)
    {
        int time = 0;

        if (index == 0)
        {
            time = (int)(Mathf.Abs((int)(rot - 360)) / hoursToDegrees);

            if (Alarm.instance.PM)
            {
                time = time + 12;

                //if (time == 12)
                //    time = 0;
            }
            else{
                //if (time == 0)
                //    time = 12;
            }
        }

        if (index == 1)
        {
            time = (int)(Mathf.Abs((int)(rot - 360)) / minutesToDegrees);
        }

        if (index == 2)
        {
            time = (int)(Mathf.Abs((int)(rot - 360)) / secondsToDegrees);
        }
        return time;
    }

    public void InteractDigital(bool state)
    {
        ClockDigitalHoursText.interactable = state;
        ClockDigitalMinutesText.interactable = state;
        ClockDigitalSecondsText.interactable = state;
    }

    public void OnChangeDigital(int index)
    {
        if (index == 0)
        {
            if (ClockDigitalHoursText.text.Length < 2)
                return;

            int t = 0;
            bool canParse = int.TryParse(ClockDigitalHoursText.text, out t);

            if (canParse)
            {
                if (t > 23)
                    t = 0;
                if (t < 0)
                    t = 0;
            }
            else
            {
                t = 0;
            }

            ClockDigitalHoursText.text = t.ToString("00");
            Alarm.instance.ArrowsTime[0] = t;
        }

        if (index == 1)
        {
            if (ClockDigitalMinutesText.text.Length < 2)
                return;

            int t = 0;
            bool canParse = int.TryParse(ClockDigitalMinutesText.text, out t);

            if (canParse)
            {
                if (t > 59)
                    t = 0;
                if (t < 0)
                    t = 0;
            }
            else
            {
                t = 0;
            }

            ClockDigitalMinutesText.text = t.ToString("00");
            Alarm.instance.ArrowsTime[1] = t;
        }

        if (index == 2)
        {
            if (ClockDigitalSecondsText.text.Length < 2)
                return;

            int t = 0;
            bool canParse = int.TryParse(ClockDigitalSecondsText.text, out t);

            if (canParse)
            {
                if (t > 59)
                    t = 0;
                if (t < 0)
                    t = 0;
            }
            else
            {
                t = 0;
            }

            ClockDigitalSecondsText.text = t.ToString("00");
            Alarm.instance.ArrowsTime[2] = t;
        }

        UpdateTime(Alarm.instance.ArrowsTime[0], Alarm.instance.ArrowsTime[1], Alarm.instance.ArrowsTime[2]);
    }

    public void OnEndChangeDigital(int index)
    {
        if (index == 0)
        {
            int t = int.Parse(ClockDigitalHoursText.text);
            ClockDigitalHoursText.text = t.ToString("00");
        }

        if (index == 1)
        {
            int t = int.Parse(ClockDigitalMinutesText.text);
            ClockDigitalMinutesText.text = t.ToString("00");
        }

        if (index == 2)
        {
            int t = int.Parse(ClockDigitalSecondsText.text);
            ClockDigitalSecondsText.text = t.ToString("00");
        }
    }
}
