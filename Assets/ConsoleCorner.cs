using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ConsoleCorner : MonoBehaviour, IDragHandler, IEventSystemHandler
{
    public Vector2 position;

    public bool shouldMove = true;

    public delegate void DragAction();
    public static event DragAction IsDragging;

    public void OnDrag(PointerEventData eventData)
    {
        if(Mouse.current.delta.ReadValue().x > 0 || Mouse.current.delta.ReadValue().y > 0) { ConsoleCornerManager.Instance.canBeScaled = true; shouldMove = true; }
        if (!shouldMove) { return; }

        //Makes the square move
        position += Mouse.current.delta.ReadValue();
        transform.position += new Vector3(Mouse.current.delta.ReadValue().x, Mouse.current.delta.ReadValue().y, 0);

        //If it has an function it calls whenever the action gets triggered
        if(IsDragging != null)
        {
            //Call the action
            IsDragging?.Invoke();
        }
    }
}
