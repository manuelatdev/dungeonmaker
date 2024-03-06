using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoAtaqueScript : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    public Animator animatorHero;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            animatorHero.SetTrigger("Atack");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest"))
        {
            animatorHero.SetTrigger("Atack");
        }
    }
}
