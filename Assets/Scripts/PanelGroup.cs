using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    public GameObject[] panels;

    public NavMainMenu navMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        ShowCurrentPanel();
    }

    public void ShowCurrentPanel()
    {
        int index = navMainMenu.navIndex;
        for (int i = 0; i < panels.Length; i++)
        {
            if(i == index)
            {
                panels[i].gameObject.SetActive(true);
            }
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }
    }
}
