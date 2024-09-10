using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource SFX;
    [SerializeField] private AudioClip AlarmSnd;

    public static AudioController instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;

        SFX = GetComponent<AudioSource>();
    }

    public void PlayAlarmSnd(bool state)
    {
        if (state)
        {
            SFX.clip = AlarmSnd;
            SFX.loop = true;
            SFX.Play();
        }
        else{
            SFX.Stop();
        }
    }
}
