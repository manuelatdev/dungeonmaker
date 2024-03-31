using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private Animator anim;
    private int tutoPagina;
    private AudioSource paginaAudio;

    private void Start()
    {
        paginaAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    public void  ContinueTuto()
    {
        if (tutoPagina == 0)
        {
            anim.SetTrigger("TUTO2");
            tutoPagina++;
            paginaAudio.Play();
        }
        else if (tutoPagina == 1)
        {
            anim.SetTrigger("TUTO3");
            tutoPagina++;
            paginaAudio.Play();

        }
        else if (tutoPagina == 2)
        {
            anim.SetTrigger("TUTOEND");
            tutoPagina=0;
            paginaAudio.Play();

        }
    }
    
    public void TutoEnd()
    {
        anim.SetTrigger("RESET");
        gameObject.SetActive(false);
    }
}
