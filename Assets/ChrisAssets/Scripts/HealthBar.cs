using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HpSlider;

    public Gradient HpGradient;

    public Image HpFill;

    // Use at void Start
    public void SetMaxHealth(int _health)
    {
        HpSlider.maxValue = _health;
        HpSlider.value = _health;

        HpFill.color = HpGradient.Evaluate(1.0f);
    }

    // Use in void Update
    public void UpdateHealth(int _health)
    {
        HpSlider.value = _health;

        HpFill.color = HpGradient.Evaluate(HpSlider.normalizedValue);
    }
}