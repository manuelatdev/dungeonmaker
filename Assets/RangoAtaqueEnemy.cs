using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueEnemy : MonoBehaviour
{
    [SerializeField]
    private Animator anim;


    private bool atacando;

   
    private void OnTriggerStay2D(Collider2D collision)

    {
        if (!atacando && ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("HeroBody"))
            {
                anim.SetBool("Attack", true);
                atacando = true;

            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (atacando)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("HeroBody"))
            {
                atacando = false;
                anim.SetBool("Attack", false);
            }
            
        }
    }
    
}
