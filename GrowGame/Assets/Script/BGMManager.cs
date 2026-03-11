using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioSource bgmsource;

    private AudioClip lastUseClip;

    private void Update()
    {
        if (!bgmsource.isPlaying)
        {
            RandeomPlay();
        }
    }

    private void RandeomPlay()
    {
        bgmsource.clip = bgmClips[Random.Range(0, bgmClips.Length)];
        if (lastUseClip == bgmsource.clip) return;
        bgmsource.Play();
        lastUseClip = bgmsource.clip;
    }
}
