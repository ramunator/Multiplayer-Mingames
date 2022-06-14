using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShortly : MonoBehaviour
{
    public List<Transform> objectsToEnable = new List<Transform>();

    public float DelayTime = 2;

    [Range(0, 1)]
    public float randomness;

    float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;

        float RandomnessValue = Random.Range(-randomness, randomness);

        if(currentTime >= DelayTime + RandomnessValue)
        {
            currentTime = 0;
            
            int test = Random.Range(0, objectsToEnable.Capacity);

            objectsToEnable[test].gameObject.SetActive(true);

            objectsToEnable.RemoveAt(test);
        }
    }
}
