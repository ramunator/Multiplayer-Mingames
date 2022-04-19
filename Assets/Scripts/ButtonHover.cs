using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.InputSystem;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    [Header("Config")]
    public InputAction triggerButtonAction;
    public Image OnMouseEnterSprite;
    public float alphaSmoothing;
    public bool interactable = true;

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

        triggerButtonAction.performed += TriggerButton;
    }

    public void TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("Triggered Button");
        if (!interactable) { return; }
        
        OnButtonClickedEvent?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioSource SFX)
    {
        SFX.Play();
    }

    #region ClickButtonFunctions



    #endregion

    #region ChangeButtonFunctions

    public void SetObjectScale(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .2f);
    }

    public void SetObjectScaleZero(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), .2f);
    }

    public void SetShadowState(bool active)
    {
        transform.Find("Background").GetComponent<Shadow>().enabled = active;
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
        CanvasGroup group = GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(group, newAlpha, .12f);
    }

    public void AlphaChangeImage(float newAlpha)
    {
        float vel = 10;
        Color imageColor = GetComponent<Image>().color;
        imageColor.a = newAlpha;
    }

    public void AddBorder(bool active)
    {
        if (active) {
            OnMouseEnterSprite.gameObject.SetActive(true);
        }
        if (!active) 
        {
            OnMouseEnterSprite.gameObject.SetActive(false);
        }
        
    }

    #endregion

    #region Pointer

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) { return; }

        OnButtonClickedEvent?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerHoverEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitedEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
    }
    #endregion
}
