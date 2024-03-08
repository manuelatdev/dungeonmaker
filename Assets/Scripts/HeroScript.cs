using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    private int[] experienceLevels = {0, 5, 15, 30};

    [SerializeField]
    private int health;

    private int totalHealth;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int def;

    [SerializeField]
    private int range;

   
    [SerializeField]
    private float attackSpeed;


    private ScriptMovimientoHeroe movimientoScript;
    private int heroGold;

    private int heroExperience;

    private int heroLevel;

    // UI
    [SerializeField]
    private TextMeshProUGUI goldLabel;

    [SerializeField]
    private TextMeshProUGUI attackLabel;

    [SerializeField]
    private TextMeshProUGUI healthLabel;

    [SerializeField]
    private TextMeshProUGUI defLabel;

    [SerializeField]
    private TextMeshProUGUI speedLabel;

    [SerializeField]
    private TextMeshProUGUI levelLabel;

    [SerializeField]
    private TextMeshProUGUI experienceLabel;

    [SerializeField]
    private Image experienceBar;
    public int getDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        movimientoScript = GetComponent<ScriptMovimientoHeroe>();
        heroLevel = 1;
        levelLabel.text = heroLevel.ToString();
        ActualizarMarcador();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnemyDied(GameObject enemyRef)
    {
        BasicEnemy basicEnemy = enemyRef.GetComponent<BasicEnemy>();

        heroGold += enemyRef.GetComponent<BaseEntity>().getGold();
        if (basicEnemy != null)
        {
            heroExperience += basicEnemy.getExperience(); 
        }
        ActualizarMarcador();
        movimientoScript.NextTarget();


    }
    private void ActualizarMarcador()
    {
        
            
            experienceBar.fillAmount = (float)((float)heroExperience / (float)experienceLevels[heroLevel]);

            if (heroExperience >= experienceLevels[heroLevel])
            {
                heroExperience -= experienceLevels[heroLevel];
                heroLevel++;
                damage++;
                health += 5;
                levelLabel.text = heroLevel.ToString();
                experienceBar.fillAmount = (float)((float)heroExperience / (float)experienceLevels[heroLevel]);
            }
        
        goldLabel.text = heroGold.ToString();
        attackLabel.text = damage.ToString();
        healthLabel.text = health.ToString();
        defLabel.text = def.ToString();
        speedLabel.text = attackSpeed.ToString();
        experienceLabel.text = heroExperience + "/" + experienceLevels[heroLevel];
        


    }


}
