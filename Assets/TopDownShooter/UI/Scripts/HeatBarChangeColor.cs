using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBarChangeColor : MonoBehaviour
{
    public Image image;
    public void OnValueChanged(float value)
    {
        image.color = new Color(1, 1 - value, 1 - value, 100);
    }
}
