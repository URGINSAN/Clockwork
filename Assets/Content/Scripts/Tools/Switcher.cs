using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Switcher : MonoBehaviour
{
    public bool Switch;
    [Space]
    [SerializeField] private Image Background;
    [SerializeField] private Color BackgroundColOn;
    [SerializeField] private Color BackgroundColOff;
    [Space]
    [SerializeField] private Transform Point;
    [SerializeField] private Vector2 PointPosOn;
    [SerializeField] private Vector2 PointPosOff;
    [Space]
    public UnityEvent On;
    public UnityEvent Off;

    [SerializeField] private float ColorSpeed = 5;
    [SerializeField] private float PointSpeed = 10;

    private void Update()
    {
        if (Switch)
        {
            Background.color = Color.Lerp(Background.color, BackgroundColOn, ColorSpeed * Time.deltaTime);
            Point.localPosition = Vector2.Lerp(Point.localPosition, PointPosOn, PointSpeed * Time.deltaTime);
        }
        else
        {
            Background.color = Color.Lerp(Background.color, BackgroundColOff, ColorSpeed * Time.deltaTime);
            Point.localPosition = Vector2.Lerp(Point.localPosition, PointPosOff, PointSpeed * Time.deltaTime);
        }
    }

    public void OnPress()
    {
        Switch = !Switch;

        if (Switch)
            On?.Invoke();
        if (!Switch)
            Off?.Invoke();
    }
}
