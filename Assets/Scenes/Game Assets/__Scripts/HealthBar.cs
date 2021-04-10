using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    //Sets the max slider value to a value
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    //Sets the current slider value to a specific value
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
