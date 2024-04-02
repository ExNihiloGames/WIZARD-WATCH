using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public string soundName = "";

    [ContextMenu("Test sound")]
    public void PlaySound()
    {
        if (soundName != "")
        {
            AudioManager.Instance?.Play(soundName);
        }
    }
}
