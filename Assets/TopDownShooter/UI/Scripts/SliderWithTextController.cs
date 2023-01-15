using System.Collections;
using UnityEngine.UI;
using UnityEngine;



public class SliderWithTextController : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public void ChangeSliderValue(float value)
    {
        if(slider == null) return;

        // Debug.Log(value);
        slider.value = value/100;

        if(text == null) return;

        text.text = value.ToString();
    }
}
