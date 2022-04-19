using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshUpdater : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<Mesh> playerMesh = new List<Mesh>();
    public List<Material> playerMats = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer.sharedMesh = playerMesh[PlayerPrefs.GetInt("PlayerId")];
        skinnedMeshRenderer.material = playerMats[PlayerPrefs.GetInt("PlayerMatId")];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
