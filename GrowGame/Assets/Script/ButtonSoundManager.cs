using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip clicksounds;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioSource buildSoundSource;

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
        audioSource.PlayOneShot(clicksounds);
    }

    public void buildSoundPlay()
    {
        buildSoundSource.Play();
    }
}
