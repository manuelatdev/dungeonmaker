using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource victorySound;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private HeroScript scriptHero;
    [SerializeField]
    private TextMeshProUGUI health;
    [SerializeField]
    private TextMeshProUGUI gold;
    [SerializeField]
    private TextMeshProUGUI exp;
    [SerializeField]
    private TextMeshProUGUI lvl;
    [SerializeField]
    private ScriptSmallDescriptions descriptionScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayAudio()
    {
        victorySound.Play();
    }
    public void GoVictory()
    {
        anim.SetTrigger("VictoryOn");
    }
    public void GetStats()
    {
        int lvlConseguido = (scriptHero.heroLevel - scriptHero.initialHeroLevel);
        health.text = scriptHero.healthTotalRestada.ToString();
        gold.text = "+"+(scriptHero.heroGold - scriptHero.initialHeroGold);
        exp.text = "+" + scriptHero.expTotalObtenida;
        lvl.text = "+" + lvlConseguido;
        descriptionScript.descriptionText = "+" + (lvlConseguido * 5) + " MAX HEALTH\n" + "+" + lvlConseguido +" ATTACK";
    }
}
