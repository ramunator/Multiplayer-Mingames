using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ResizableWindowCorner : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 position;

    public Texture2D defaultCursor;

    public Texture2D resizeCursor;

    public bool shouldMove = true;

    public delegate void DragAction();
    public static event DragAction IsDragging;

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(Mouse.current.delta.ReadValue().normalized.y  );
        if(Mouse.current.delta.ReadValue().normalized.x > 0.1f && ResizableWindowManager.Instance.background.rect.size.x >= ResizableWindowManager.Instance.rectSizeMax.x) { ResizableWindowManager.Instance.canBeScaled = true; shouldMove = true; }
        if(Mouse.current.delta.ReadValue().normalized.y > 0.1f && ResizableWindowManager.Instance.background.rect.size.y >= ResizableWindowManager.Instance.rectSizeMax.y) { Debug.Log("Test"); ResizableWindowManager.Instance.canBeScaled = true; shouldMove = true; }
        if(Mouse.current.delta.ReadValue().normalized.x < 0.1f && ResizableWindowManager.Instance.background.rect.size.x <= ResizableWindowManager.Instance.rectSizeMin.x) { ResizableWindowManager.Instance.canBeScaled = true; shouldMove = true; }
        if(Mouse.current.delta.ReadValue().normalized.y < 0.1f && ResizableWindowManager.Instance.background.rect.size.y <= ResizableWindowManager.Instance.rectSizeMin.y) { ResizableWindowManager.Instance.canBeScaled = true; shouldMove = true; }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(resizeCursor, new Vector2(64, 64), CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }
}
