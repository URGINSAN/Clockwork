using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class GetTime : MonoBehaviour
{
    public enum Type
    {
        TimeApi1 = 0,
        TimeApi2 = 1
    }
    public Type type;

    private string TimerUrl1 = "https://timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
    private string TimerUrl2 = "https://api.ipgeolocation.io/timezone?apiKey=bd87746bbd9c44079b65d861720fe42b&tz=Europe/Moscow";
    [Space]
    public TimeParseUrl1 Time1;
    public TimeParseUrl2 Time2;
    [Space]
    public int Hours;
    public int Minutes;
    public int Seconds;

    private void Start()
    {
        ActualTime();
    }

    #region api 1
    IEnumerator GetRealDateTimeFromAPI1()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(TimerUrl1);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            //Debug.Log(webRequest.downloadHandler.text);
            Time1 = ParseUrl1FromJSON(webRequest.downloadHandler.text);

            Hours = Time1.hour;
            Minutes = Time1.minute;
            Seconds = Time1.seconds;

            UIController.instance.UpdateTime(Hours, Minutes, Seconds);
        }
    }

    public static TimeParseUrl1 ParseUrl1FromJSON(string jsonString)
    {
        return JsonUtility.FromJson<TimeParseUrl1>(jsonString);
    }

    #endregion

    #region api 2
    IEnumerator GetRealDateTimeFromAPI2()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(TimerUrl2);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            //Debug.Log(webRequest.downloadHandler.text);
            Time2 = ParseUrl2FromJSON(webRequest.downloadHandler.text);

            string[] time = Time2.time_24.Split(':');
            Hours = int.Parse(time[0]);
            Minutes = int.Parse(time[1]);
            Seconds = int.Parse(time[2]);

            UIController.instance.UpdateTime(Hours, Minutes, Seconds);
        }
    }

    public static TimeParseUrl2 ParseUrl2FromJSON(string jsonString)
    {
        return JsonUtility.FromJson<TimeParseUrl2>(jsonString);
    }

    #endregion

    public void ActualTime()
    {
        if (type == Type.TimeApi1)
            StartCoroutine(GetRealDateTimeFromAPI1());

        if (type == Type.TimeApi2)
            StartCoroutine(GetRealDateTimeFromAPI2());

        IEnumerator IE()
        {
            yield return new WaitForSeconds(3600);
            ActualTime();
        }
        StartCoroutine(IE());
    }
}

[Serializable]
public class TimeParseUrl1
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int seconds;
    public int milliSeconds;
    public string dateTime;
    public string date;
    public string time;
    public string timeZone;
    public string dayOfWeek;
    public bool dstActive;
}

[Serializable]
public class TimeParseUrl2
{
    public string timezone;
    public int timezone_offset;
    public int timezone_offset_with_dst;
    public string date;
    public string date_time;
    public string date_time_txt;
    public string date_time_wti;
    public string date_time_ymd;
    public string date_time_unix;
    public string time_24;
    public string time_12;
    public int week;
    public int month;
    public int year;
    public int year_abbr;
    public bool is_dst;
    public int dst_savings;
    public bool dst_exists;
    public bool dst_start;
    public bool dst_end;
}
