using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{

    
    public Slider slider;

    //Max XP set function
    public void SetMaxXP(int xp)
    {
        slider.maxValue = xp;
    }

    //This will set the slider xp to a certain value
    public void SetXP(float xp)
    {
        slider.value = xp;
    }

    //This will add a specific amount of xp to the current valur
    public void IncrementXP(float xp) {
        SetXP(slider.value + xp);
    }

    //This will reset the xp to 0
    public void ResetXP() {
        slider.value = 0;      
    }

    //Returns if the slider is currently full
    public bool IsMax() {
        if (slider.maxValue == slider.value) {
            return true;
        }
        return false;
    }
    
}
