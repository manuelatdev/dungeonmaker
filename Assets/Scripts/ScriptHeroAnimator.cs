using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHeroAnimator : MonoBehaviour
{
    ScriptMovimientoHeroe heroeScript;
    [SerializeField]
    private AudioSource swordAtackSound;
    [SerializeField]
    private AudioSource footStepsSound;
    private void Start()
    {
        heroeScript = GetComponentInParent<ScriptMovimientoHeroe>();
    }
    public void Ataque()
    {
        swordAtackSound.pitch = Random.Range(1f, 1.3f);
        swordAtackSound.Play();
        heroeScript.AttackToEnemy();
    }
    public void WalkingStepsSound()
    {
        footStepsSound.pitch = Random.Range(1f, 1.2f);
        footStepsSound.volume = Random.Range(0.1f, 0.17f);

        footStepsSound.Play();
    }
}
