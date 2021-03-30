using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{

    
    public Slider slider;

    public void SetMaxXP(int xp)
    {
        slider.maxValue = xp;
        //slider.value = xp;
    }

    public void SetXP(float xp)
    {
        slider.value = xp;
    }

    public void IncrementXP(float xp) {
        SetXP(slider.value + xp);
    }

    public void ResetXP() {
        slider.value = 0;
        
    }

    public bool IsMax() {
        if (slider.maxValue == slider.value) {
            return true;
        }
        return false;
    }
    
}
