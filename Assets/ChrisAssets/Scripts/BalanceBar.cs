using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
    public Slider BpSlider;

    public Gradient BpGradient;

    public Image BpFill;

    // Use at void Start
    public void SetMinBalance(float _balance)
    {
        BpSlider.minValue = _balance;
        BpSlider.maxValue = 100;
        BpSlider.value = _balance;

        BpFill.color = BpGradient.Evaluate(1.0f);
    }

    // Use in void Update
    public void UpdateBalance(float _balance)
    {
        BpSlider.value = _balance;
        BpFill.color = BpGradient.Evaluate(BpSlider.normalizedValue);
    }
}