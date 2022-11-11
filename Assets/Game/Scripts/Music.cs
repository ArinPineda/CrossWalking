using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
   

    public void stopMusic()
    {
        if (gameObject.GetComponent<AudioSource>().mute == false)
        {
            gameObject.GetComponent<AudioSource>().mute = true;

        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = false;

        }
    }
}
