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

    public static AudioSource cardTaken;
    public static AudioSource cardPlaced;


    [SerializeField]
    private AudioSource cardTakenRef;
    [SerializeField]
    private AudioSource cardPlacedRef;
    [SerializeField]
    private AudioSource slimeAttackRef;
    [SerializeField]
    private AudioSource slimeDeadRef;
    private void Awake()
    {
        slimeAttack = slimeAttackRef;
        slimeDead = slimeDeadRef;

        cardTaken = cardTakenRef;
        cardPlaced = cardPlacedRef;
    }
}
