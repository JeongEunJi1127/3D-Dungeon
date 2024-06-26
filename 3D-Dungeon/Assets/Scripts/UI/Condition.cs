using System;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;

    private void Awake()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float amount)
    {
        curValue = Math.Min(curValue+amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue-amount, 0);
    }
}
