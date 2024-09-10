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
    [SerializeField] private GameObject AlarmSwitcherAMPM;
    [SerializeField] private GameObject Alarming;
    [SerializeField] private ArrowDrag[] Arrows;
    public int[] ArrowsTime;
    public bool PM;

    public static Alarm instance;
    public bool AlarmSetted;
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

    private void Start()
    {
        if (PlayerPrefs.HasKey("AlarmTime"))
        {
            AlarmTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("AlarmTime")));
            AlarmSetted = true;

            ArrowsTime[0] = AlarmTime.Hour;
            ArrowsTime[1] = AlarmTime.Minute;
            ArrowsTime[2] = AlarmTime.Second;

            ConfirmAlarm(true);
            print(AlarmTime);
        }
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

        if (AlarmSetted)
        {
            if (GetTime.instance.TimeCurrent >= AlarmTime)
            {
                OnAlarm(true);
                AlarmSetted = false;
            }
        }
    }

    public void ClickAlarm()
    {
        GetTime.instance.OnSetAlarm(true);

        AlarmGO.SetActive(false);
        AlarmConfirmGO.SetActive(true);
        AlarmSwitcherAMPM.SetActive(true);

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
            if (!AlarmSetted)
            {
                AlarmTime = new DateTime(GetTime.instance.TimeCurrent.Year,
                    GetTime.instance.TimeCurrent.Month,
                    GetTime.instance.TimeCurrent.Day,
                    ArrowsTime[0],
                    ArrowsTime[1],
                    ArrowsTime[2]);

                if (AlarmTime < GetTime.instance.TimeCurrent)//Если время будильника в этот день уже прошло, то ставим будильник на след день
                {
                    AlarmTime = new DateTime(GetTime.instance.TimeCurrent.Year,
                   GetTime.instance.TimeCurrent.Month,
                   GetTime.instance.TimeCurrent.Day + 1,
                   ArrowsTime[0],
                   ArrowsTime[1],
                   ArrowsTime[2]);
                }

                PlayerPrefs.SetString("AlarmTime", AlarmTime.ToBinary().ToString());
            }

            ClockIcon.SetActive(true);
            AlarmConfirmText.text = "Оставить";
            AlarmText.text = $"{AlarmTime.Hour.ToString("00")}:{AlarmTime.Minute.ToString("00")}:{AlarmTime.Second.ToString("00")}";

            print("Alarm setted: " + AlarmTime);
            AlarmSetted = true;
        }
        else
        {
            ClockIcon.SetActive(false);
            AlarmConfirmText.text = "Применить";
            AlarmText.text = "Будильник";

            AlarmSetted = false;
            PlayerPrefs.DeleteKey("AlarmTime");
        }

        GetTime.instance.OnSetAlarm(false);

        AlarmGO.SetActive(true);
        AlarmConfirmGO.SetActive(false);
        AlarmSwitcherAMPM.SetActive(false);
        OnEdit = false;
    }

    public void SetArrowTime(int index, float rot)
    {
        ArrowsTime[index] = UIController.instance.ArrowTime(index, rot);
        UIController.instance.AdjustAlarmArrows(ArrowsTime[0], ArrowsTime[1], ArrowsTime[2]);
    }

    public void SetAMPM(bool pm)
    {
        PM = pm;
    }

    public void OnAlarm(bool state)
    {
        Alarming.SetActive(state);
        AudioController.instance.PlayAlarmSnd(state);
        AlarmSetted = false;
        ConfirmAlarm(false);
        PlayerPrefs.DeleteKey("AlarmTime");
    }
}
