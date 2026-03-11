using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip Clicksounds;
    public AudioSource AudioSource;

    void Start()
    {
        var buttons = FindObjectsOfType<Button>();
        foreach (var b in buttons) AddListenerToButton(b);
    }

    public void RegisterButton(Button b)
    {
        if (b == null) return;
        AddListenerToButton(b);
    }

    void AddListenerToButton(Button b)
    {
        b.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        AudioSource.PlayOneShot(Clicksounds);
    }
}
