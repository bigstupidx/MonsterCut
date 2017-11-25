using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceAudioListener : MonoBehaviour
{
    public UISprite soundSprite;
    public UISprite noSoundSprite;

    private void Start()
    {
        setSprites();
    }

    void setSprites()
    {
        soundSprite.gameObject.SetActive(!AudioListener.pause);
        noSoundSprite.gameObject.SetActive(AudioListener.pause);
    }

    public void soundToggle()
    {
        AudioListener.pause = !AudioListener.pause;
        setSprites();
    }
}

