using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

public class ButtonHover3D : MonoBehaviour
{
    [Header("Config")]
    public Image OnMouseEnterSprite;
    public float alphaSmoothing;
    public LayerMask buttonLayer;
    public Vector3 mouseDir;

    [Space]

    [Header("Events")]
    public UnityEvent OnPointerHoverEvent;
    public UnityEvent OnPointerExitedEvent;
    public UnityEvent OnButtonClickedEvent;
    public UnityEvent OnPointerUpEvent;
    public UnityEvent OnPointerDownEvent;


    [SerializeField] Vector3 lastSelectedScale = Vector3.one;
    [SerializeField] private Image defaultImage;

    float lerp = 1;

    // Start is called before the first frame update
    void Start()
    {
        defaultImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 999f, buttonLayer))
        {
            if(hit.collider.gameObject != this.gameObject) { return; }

            Debug.Log("Hit Something");
            if (Mouse.current.leftButton.isPressed)
            {
                OnPointerDownEvent?.Invoke();
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                OnPointerUpEvent?.Invoke();
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnButtonClickedEvent?.Invoke();
            }

            OnPointerHoverEvent?.Invoke();


        }
        else
        {
            OnPointerExitedEvent?.Invoke();
        }
    }

    public void PlaySFX(AudioSource SFX)
    {
        SFX.Play();
    }

    #region ChangeButtonFunctions

    public void SetObjectScale(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .2f);
    }

    public void SetScale(float newScale)
    {
        LeanTween.scale(gameObject, new Vector3(newScale, newScale, newScale), .2f);
    }

    public void FastScale(float newScale)
    {
        LeanTween.scale(gameObject, new Vector3(newScale, newScale, newScale), .2f);
    }

    public void AlphaChangeButton(float newAlpha)
    {
        Debug.Log("Test");
        CanvasGroup group = GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(group, newAlpha, .12f);
    }

    public void AddBorder(bool active)
    {
        if (active)
        {
            OnMouseEnterSprite.gameObject.SetActive(true);
        }
        if (!active)
        {
            OnMouseEnterSprite.gameObject.SetActive(false);
        }

    }

    #endregion
}
