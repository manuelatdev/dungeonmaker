using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioSource slimeAttack;
    public static AudioSource slimeDead;

    public static AudioSource skullDead;
    public static AudioSource skullAttack;

    public static AudioSource archerDead;
    public static AudioSource archerAttack;

    public static AudioSource wizardDead;
    public static AudioSource wizardAttack;

    [SerializeField]
    private AudioSource slimeAttackRef;
    [SerializeField]
    private AudioSource slimeDeadRef;
    private void Start()
    {
        slimeAttack = slimeAttackRef;
        slimeDead = slimeDeadRef;
    }
}
