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
        swordAtackSound.pitch = Random.Range(0.9f, 1.3f);
        swordAtackSound.Play();
        heroeScript.AttackToEnemy();
    }
}
