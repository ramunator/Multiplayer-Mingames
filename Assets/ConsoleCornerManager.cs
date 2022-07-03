using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConsoleCornerManager : MonoBehaviour
{
    public static ConsoleCornerManager Instance { get; private set; }

    public bool consoleEnabled = true;
    public bool debugEnabled = true;

    public delegate void PositionOverTheBorder();
    public static event PositionOverTheBorder PositionIsOverTheBorder;

    [SerializeField] private ConsoleCorner leftBottomCorner;

    [SerializeField] private RectTransform background;

    public Vector2 rectSizeMax;

    public Vector2 rectSizeOnStart;

    private Vector2 rectSizeOld;

    private Vector2 rectSizeNew;

    public bool canBeScaled = true;

    // Start is called before the first frame update
    void Start()
    {
        background.sizeDelta = rectSizeOnStart;

        Instance = this;

        if (debugEnabled)
        {
            leftBottomCorner.GetComponent<Image>().enabled = true;
        }
        else
        {
            leftBottomCorner.GetComponent<Image>().enabled = false;
        }
    }

    private void OnEnable()
    {
        if (consoleEnabled == true)
        {
            ConsoleCorner.IsDragging += UpdateSize;
            ConsoleCornerManager.PositionIsOverTheBorder += ResetBackgroundSize;
        }
    }

    private void OnDisable()
    {
        ConsoleCorner.IsDragging -= UpdateSize;
        ConsoleCornerManager.PositionIsOverTheBorder -= ResetBackgroundSize;
    }

    private void UpdateSize()
    {
        if(background.rect.width > rectSizeMax.x && leftBottomCorner.position.x > -(rectSizeMax.x - rectSizeOnStart.x) && !canBeScaled) { PositionIsOverTheBorder?.Invoke(); return;  }
        if (background.rect.height > rectSizeMax.y && leftBottomCorner.position.y > -(rectSizeMax.y - rectSizeOnStart.y) && !canBeScaled) { PositionIsOverTheBorder?.Invoke(); return; }

        background.sizeDelta = new Vector2(rectSizeOnStart.x - leftBottomCorner.position.x * 1.25f, rectSizeOnStart.y - leftBottomCorner.position.y * 1.25f);
        rectSizeOld = background.sizeDelta;

        if(rectSizeNew != Vector2.zero)
        {
            background.transform.localPosition -= new Vector3((rectSizeOld.x - rectSizeNew.x) / 2, (rectSizeOld.y - rectSizeNew.y) / 2, 0);
        }

        rectSizeNew = background.sizeDelta;
    }

    public virtual void ResetBackgroundSize()
    {
        if(leftBottomCorner != null)
        {
            canBeScaled = false;
            leftBottomCorner.shouldMove = false;
        }
        if (!debugEnabled) { return; }
        Debug.Log("Background Size Is Over The Border Resseting It");
    }
}
