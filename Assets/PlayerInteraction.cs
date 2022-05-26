using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public UnityEvent onInteracted;

    [TextArea] public string description;

    public void DoStuff()
    {
        onInteracted?.Invoke();
    }
}
