using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public static ToolTipSystem Instance { get; private set; }

    [SerializeField] private ToolTip toolTip;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show(string content, string header = "")
    {
        Instance.toolTip.SetText(content, header);
        Instance.toolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.toolTip.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
