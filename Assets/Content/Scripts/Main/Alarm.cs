using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Alarm : MonoBehaviour
{
    private bool OnEdit;
    [SerializeField] private GameObject AlarmGO;
    [SerializeField] private GameObject AlarmConfirmGO;
    [SerializeField] private ArrowDrag[] Arrows;
    public int[] ArrowsTime;

    public static Alarm instance;
    public DateTime AlarmTime;

    public Text AlarmText;
    public Text AlarmConfirmText;
    public GameObject ClockIcon;
    [Space]
    public Image AnalogImg;
    public Color AnalogColNorm;
    public Color AnalogColEdit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (OnEdit)
        {
            AnalogImg.color = Color.Lerp(AnalogImg.color, AnalogColEdit, Time.deltaTime * 3);
        }
        else
        {
            AnalogImg.color = Color.Lerp(AnalogImg.color, AnalogColNorm, Time.deltaTime * 3);
        }
    }

    public void ClickAlarm()
    {
        GetTime.instance.OnSetAlarm(true);

        AlarmGO.SetActive(false);
        AlarmConfirmGO.SetActive(true);

        for (int i = 0; i < Arrows.Length; i++)
            Arrows[i].CanDrag = true;

        OnEdit = true;
    }

    public void ConfirmAlarm(bool state)
    {
        for (int i = 0; i < Arrows.Length; i++)
            Arrows[i].CanDrag = false;

        if (state)
        {
            AlarmTime = new DateTime(GetTime.instance.TimeCurrent.Year,
                GetTime.instance.TimeCurrent.Month,
                GetTime.instance.TimeCurrent.Day,
                ArrowsTime[0],
                ArrowsTime[1],
                ArrowsTime[2]);

            ClockIcon.SetActive(true);
            AlarmConfirmText.text = "Оставить";
            AlarmText.text = $"{ArrowsTime[0].ToString("00")}:{ArrowsTime[1].ToString("00")}:{ArrowsTime[2].ToString("00")}";
        }
        else
        {
            ClockIcon.SetActive(false);
            AlarmConfirmText.text = "Применить";
            AlarmText.text = "Будильник";
        }

        GetTime.instance.OnSetAlarm(false);

        AlarmGO.SetActive(true);
        AlarmConfirmGO.SetActive(false);
        OnEdit = false;
    }

    public void SetArrowTime(int index, float rot)
    {
        ArrowsTime[index] = UIController.instance.ArrowTime(index, rot);
        UIController.instance.AdjustAlarmArrows(ArrowsTime[0], ArrowsTime[1], ArrowsTime[2]);
    }
}
