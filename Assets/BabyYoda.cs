using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyYoda : NetworkBehaviour
{
    private bool isUsingLightsaber = false;

    public Transform lightsaber;

    public void TriggerUseLightsaber()
    {
        CmdTriggerLightsaber();
    }

    [Command(requiresAuthority =false)]
    private void CmdTriggerLightsaber()
    {
        if(isUsingLightsaber == false)
        {
            lightsaber.GetComponent<Animator>().SetTrigger("Open");
            isUsingLightsaber = true;
            AudioManager.instance.Play("LightSaberOpen");
            AudioManager.instance.Play("LightSaberIdle");
        }
        else if(isUsingLightsaber == true)
        {
            lightsaber.GetComponent<Animator>().SetTrigger("Close");
            AudioManager.instance.Stop("LightSaberIdle");
            AudioManager.instance.Play("LightSaberClose");
            isUsingLightsaber = false;
        }
    }
}
