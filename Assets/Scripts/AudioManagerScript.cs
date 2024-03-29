using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioSource music;
    public static AudioSource click;

    public static AudioSource slimeAttack;
    public static AudioSource slimeDead;

    public static AudioSource skullDead;
    public static AudioSource skullAttack;

    public static AudioSource archerDead;
    public static AudioSource archerAttack;
    public static AudioSource archerCharge;
    public static AudioSource archerHitWall;


    public static AudioSource wizardDead;
    public static AudioSource wizardAttack;
    public static AudioSource wizardCharge;
    public static AudioSource wizardSpell;

    public static AudioSource bossDead;
    public static AudioSource bossAttack;



    public static AudioSource cardTaken;
    public static AudioSource cardPlaced;


    [SerializeField]
    private AudioSource musicRef;
    [SerializeField]
    private  AudioSource clickRef;
    [SerializeField]
    private AudioSource cardTakenRef;
    [SerializeField]
    private AudioSource cardPlacedRef;
    [SerializeField]
    private AudioSource slimeAttackRef;
    [SerializeField]
    private AudioSource slimeDeadRef;
    [SerializeField]
    private AudioSource skullAttackRef;
    [SerializeField]
    private AudioSource skullDeadRef;
    [SerializeField]
    private AudioSource wizardAttackRef;
    [SerializeField]
    private AudioSource wizardDeadRef;
    [SerializeField]
    private AudioSource wizardChargeRef;
    [SerializeField]
    private AudioSource wizardSpellRef;

    [SerializeField]
    private AudioSource archerAttackRef;
    [SerializeField]
    private AudioSource archerDeadRef;
    [SerializeField]
    private AudioSource archerChargeRef;
    [SerializeField]
    private AudioSource archerHitWallRef;
    [SerializeField]
    private AudioSource bossAttackRef;
    [SerializeField]
    private AudioSource bossDeadRef;
    private void Awake()
    {
        bossDead = bossDeadRef;
        bossAttack = bossAttackRef;

        music = musicRef;
        click = clickRef;

        slimeAttack = slimeAttackRef;
        slimeDead = slimeDeadRef;

        cardTaken = cardTakenRef;
        cardPlaced = cardPlacedRef;

        skullDead = skullDeadRef;
        skullAttack = skullAttackRef;

        wizardDead = wizardDeadRef;
        wizardAttack = wizardAttackRef;
        wizardCharge = wizardChargeRef;
        wizardSpell = wizardSpellRef;

        archerAttack = archerAttackRef;
        archerCharge = archerChargeRef;
        archerDead = archerDeadRef;
        archerHitWall = archerHitWallRef;
    }
}
