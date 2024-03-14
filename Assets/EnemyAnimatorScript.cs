using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorScript : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0); //obtiene el estado actual de la animaci�n
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f)); //inicia la animaci�n en un momento aleatorio
    }
}
