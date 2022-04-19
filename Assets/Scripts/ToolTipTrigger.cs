using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    [SerializeField] private string header;
    [TextArea] 
    [SerializeField] private string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipSystem.Show(content, header);
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale - new Vector3(.1f, .1f, .1f), .5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale,transform.localScale + new Vector3(.1f, .1f, .1f), .5f);
    }
}
