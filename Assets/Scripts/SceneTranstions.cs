using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranstions : MonoBehaviour
{
    public static SceneTranstions Instance { get; private set; }

    Animator anim;

    private void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartTranstion(sceneName));
    }

    public IEnumerator StartTranstion(string sceneName)
    {
        transform.GetChild(0).GetChild(0).position = Camera.main.transform.position - new Vector3(0, 0, -5f);
        anim.SetTrigger("Start");


        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

}
