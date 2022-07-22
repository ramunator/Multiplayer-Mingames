using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUIButton : MonoBehaviour
{
    public enum ButtonState
    {
        button,
        checkBox,
        slider
    };
    public ButtonState buttonState;
    public int rowIndex = 0;
    public int columnIndex = 0;

    public void SetRow(int index)
    {
        rowIndex = index;
    }

    public void SetColumn(int index)
    {
        columnIndex = index;
    }
}
