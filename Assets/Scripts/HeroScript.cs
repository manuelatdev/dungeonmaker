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

    [SerializeField]
    private int damage;

    [SerializeField]
    private int range;

    [SerializeField]
    private float attackSpeed;

    private int heroGold;

    private int heroExperience;

    private int heroLevel;

    // UI
    [SerializeField]
    private TextMeshProUGUI goldLabel;

    [SerializeField]
    private TextMeshProUGUI levelLabel;

    [SerializeField]
    private Image experienceBar;
    public int getDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        heroLevel = 1;
        levelLabel.text = heroLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnemyDied(GameObject enemyRef)
    {
        heroGold += enemyRef.GetComponent<BaseEntity>().getGold();

        BasicEnemy basicEnemy = enemyRef.GetComponent<BasicEnemy>();

        if (basicEnemy != null)
        {
            heroExperience += basicEnemy.getExperience();

            experienceBar.fillAmount = (float)((float)heroExperience / (float)experienceLevels[heroLevel]);

            
            if(heroExperience >= experienceLevels[heroLevel])
            {
                heroLevel++;
                levelLabel.text = heroLevel.ToString();
                experienceBar.fillAmount = 0.0f;
                heroExperience = 0;
            }
        }

        GetComponent<ScriptMovimientoHeroe>().NextTarget();

        goldLabel.text = heroGold.ToString();



        Debug.Log(heroGold.ToString());
        Debug.Log(heroExperience.ToString());

    }


}
