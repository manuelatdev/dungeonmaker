using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAtaqueHero : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    [SerializeField]
    private AudioSource swordAtackSound;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    public void Ataque()
    {
        swordAtackSound.Play();
        heroeScript.AttackToEnemy();
    }
}
