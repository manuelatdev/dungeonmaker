using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectAnimator : MonoBehaviour
{
    private AudioSource darkSoulsSound;
    [HideInInspector]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        darkSoulsSound = GetComponent<AudioSource>();
    }

    public void PlayDarkSoulsSound()
    {
        darkSoulsSound.Play();
    }
}
