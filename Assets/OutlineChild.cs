using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineChild : MonoBehaviour
{
    public float outlineWidth;
    public Color outlineColor;
    public Outline.Mode outlineMode;

    Outline _outline;

    private void Awake()
    {
        if (transform.GetChild(0).gameObject.TryGetComponent<SelectPlayer>(out SelectPlayer selectPlayer))
        {
            _outline = selectPlayer.playerMesh.gameObject.AddComponent<Outline>();
            _outline.OutlineWidth = outlineWidth;
            _outline.OutlineColor = outlineColor;
            _outline.OutlineMode = outlineMode;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
