using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAtaqueHero : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    public void Ataque()
    {
        heroeScript.AttackToEnemy();
    }
}
