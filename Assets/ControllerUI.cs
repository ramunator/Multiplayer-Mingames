using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour
{
    public NavInput controls;

    public Transform content;

    private ControllerUIButton currentButton;

    private List<ControllerUIButton> buttons = new List<ControllerUIButton>();

    [Tooltip("The max ammount of items before an new row!")]
    public int maxItemRows;

    private void Awake()
    {
        controls = new NavInput();

        controls.UISelection.ChangeCurrentSelection.performed += ctx => ChangeCurrentSelectedButton(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private IEnumerator GetControllerUICou(float waitTime)
    {
        yield return new WaitForSeconds(2f);

        int _maxItemColumns = 4;
        int _columnIndex = 0;
        int _maxItemRows = maxItemRows;
        int rowIndex = 0;
        buttons.Clear();
        for (int i = 0; i < content.childCount; i++)
        {
            int columnIndex = 0;
            buttons.Add(content.GetChild(i).GetComponent<ControllerUIButton>());

            currentButton = buttons[0];

            if (i >= _maxItemRows)
            {
                _maxItemRows += maxItemRows;
                rowIndex += 1;
            }

            if (i >= _maxItemColumns)
            {
                _maxItemColumns += maxItemRows;
                _columnIndex = 0;
                columnIndex = _columnIndex;
            }

            columnIndex = _columnIndex;

            buttons[i].SetRow(rowIndex);
            buttons[i].SetColumn(columnIndex);

            _columnIndex += 1;
        }
        currentButton.gameObject.GetComponent<ButtonHover>().OnPointerHoverEvent?.Invoke();
    }

    public void GetControllerUI(float waitTime)
    {
        StartCoroutine(GetControllerUICou(waitTime));
    }

    public void ChangeCurrentSelectedButton(Vector2 dir)
    {
        int columnIndex = currentButton.columnIndex + 1;
        int columnIndexMinus = currentButton.columnIndex - 1;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (dir.y < 0)
            {
                if (buttons[i].columnIndex == currentButton.columnIndex && buttons[i].rowIndex == currentButton.rowIndex + 1)
                {
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerExitedEvent?.Invoke();
                    currentButton = buttons[i];
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerHoverEvent?.Invoke();
                    continue;
                }
            }
            if (dir.y > 0)
            {
                if (buttons[i].columnIndex == currentButton.columnIndex && buttons[i].rowIndex == currentButton.rowIndex + -1 )
                {
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerExitedEvent?.Invoke();
                    currentButton = buttons[i];
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerHoverEvent?.Invoke();
                    continue;
                }
            }
            if (dir.x < 0)
            {
                if (buttons[i].columnIndex == columnIndexMinus && buttons[i].rowIndex == currentButton.rowIndex)
                {
                    Debug.Log("CurrentButton Column " + (currentButton.columnIndex - 1));
                    Debug.Log("Buttons[i] " + buttons[i].columnIndex);
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerExitedEvent?.Invoke();
                    currentButton = buttons[i];
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerHoverEvent?.Invoke();
                    continue;
                }
            }   
            if (dir.x > 0)
            {
                if (buttons[i].columnIndex == columnIndex && buttons[i].rowIndex == currentButton.rowIndex)
                {
                    Debug.Log("CurrentButton Column " + (currentButton.columnIndex + 1));
                    Debug.Log("Buttons[i] " + buttons[i].columnIndex);
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerExitedEvent?.Invoke();
                    currentButton = buttons[i];
                    currentButton.gameObject.GetComponent<ButtonHover>().OnPointerHoverEvent?.Invoke();
                    continue;
                }
            }
        }
    }
}
