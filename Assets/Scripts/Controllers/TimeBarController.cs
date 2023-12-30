using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarController : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetTime(float time)
    {
        slider.value = time;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;

        fill.color = gradient.Evaluate(1f);
    }
}
