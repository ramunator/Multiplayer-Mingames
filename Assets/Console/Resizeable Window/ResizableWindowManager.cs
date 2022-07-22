using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ResizableWindowManager : MonoBehaviour
{
    public static ResizableWindowManager Instance { get; private set; }

    public bool resizeWindow = true;
    public bool debugEnabled = false;

    public delegate void PositionOverTheBorder();
    public static event PositionOverTheBorder PositionIsOverTheBorder;

    [SerializeField] private ResizableWindowCorner leftBottomCorner;

    public RectTransform background;

    public Vector2 rectSizeMax;
    public Vector2 rectSizeMin;

    private Vector2 rectSizeOld;

    private Vector2 rectSizeNew;

    [HideInInspector] public bool canBeScaled = true;

    private Sprite cornerDebugTexture;

    // Start is called before the first frame update
    void Start()
    {
        background.sizeDelta = rectSizeMax;

        Instance = this;

        cornerDebugTexture = leftBottomCorner.GetComponent<Image>().sprite;

        var leftBottomCornerImage = leftBottomCorner.GetComponent<Image>();

        if (debugEnabled)
        {
            leftBottomCornerImage.sprite = cornerDebugTexture;
            leftBottomCornerImage.color = Color.black;
        }
        else
        {
            leftBottomCorner.GetComponent<Image>().sprite = null;
            leftBottomCornerImage.color = new Color32(0, 0, 0, 0);
        }
    }

    private void OnEnable()
    {
        if (resizeWindow == true)
        {
            ResizableWindowCorner.IsDragging += UpdateSize;
            ResizableWindowManager.PositionIsOverTheBorder += ResetBackgroundSize;
        }
    }

    private void OnDisable()
    {
        ResizableWindowCorner.IsDragging -= UpdateSize;
        ResizableWindowManager.PositionIsOverTheBorder -= ResetBackgroundSize;
    }

    private void UpdateSize()
    {
        if (!canBeScaled)
        {
            return;
        }

        background.sizeDelta = new Vector2(rectSizeMax.x - leftBottomCorner.position.x * 1.25f, rectSizeMax.y - leftBottomCorner.position.y * 1.25f);
        rectSizeOld = background.sizeDelta;

        if(rectSizeNew != Vector2.zero)
        {
            background.transform.localPosition -= new Vector3((rectSizeOld.x - rectSizeNew.x) / 2, (rectSizeOld.y - rectSizeNew.y) / 2, 0);
        }

        rectSizeNew = background.sizeDelta;

        if (background.rect.width > rectSizeMax.x && leftBottomCorner.position.x < 0) { PositionIsOverTheBorder?.Invoke(); canBeScaled = false; }
        if (background.rect.height > rectSizeMax.y && leftBottomCorner.position.y < 0) { PositionIsOverTheBorder?.Invoke(); canBeScaled = false; }
        if (background.rect.width < rectSizeMin.x && leftBottomCorner.position.x > (rectSizeMax.x - rectSizeMin.x)) { PositionIsOverTheBorder?.Invoke(); canBeScaled = false; }
        if (background.rect.height < rectSizeMin.y && leftBottomCorner.position.y > (rectSizeMax.y - rectSizeMin.y)) { PositionIsOverTheBorder?.Invoke(); canBeScaled = false; }

    }

    public virtual void ResetBackgroundSize()
    {
        if(leftBottomCorner != null)
        {
            leftBottomCorner.shouldMove = false;
        }
        if (!debugEnabled) { return; }
        Debug.Log("Background Size Is Over The Border Resseting It");
    }
}
