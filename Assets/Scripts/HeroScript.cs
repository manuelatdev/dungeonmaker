using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    private int[] experienceLevels = {0, 5, 15, 30};

    [SerializeField]
    private int totalHealth;

    private int actualHealth;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int def;

    [SerializeField]
    private int range;

   
    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private Image greenHealthBarImage;

    [SerializeField]
    private Image redHealthBarImage;

    [SerializeField]
    private TextMeshProUGUI healText;

    [SerializeField]
    private ParticleSystem bloodParticles;

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
        actualHealth = totalHealth;
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
        

        if (heroExperience >= experienceLevels[heroLevel])
        {
            LevelUp();
                 
        }
        
        ActualizarMarcador();
        movimientoScript.NextTarget();


    }

    private void LevelUp()
    {
        heroExperience -= experienceLevels[heroLevel];
        heroLevel++;
        damage++;
        actualHealth += 5;
        totalHealth += 5;
    }
    private void ActualizarMarcador()
    {
        levelLabel.text = heroLevel.ToString();
        experienceBar.fillAmount = (float)((float)heroExperience / (float)experienceLevels[heroLevel]);
        goldLabel.text = heroGold.ToString();
        attackLabel.text = damage.ToString();
        healthLabel.text = totalHealth.ToString();
        defLabel.text = def.ToString();
        speedLabel.text = attackSpeed.ToString();
        experienceLabel.text = heroExperience + "/" + experienceLevels[heroLevel];

    }
    public  void TakeAttack(int damage)
    {
        bloodParticles.Play();
        greenHealthBarImage.fillAmount -= (float)damage / totalHealth;
        actualHealth -= damage;
        healText.text = actualHealth + " / " + totalHealth;
        StopCoroutine(AnimateHealthBarDecrease());
        StartCoroutine(AnimateHealthBarDecrease());
    }
    IEnumerator AnimateHealthBarDecrease()
    {
        float elapsedTime = 0;
        float startValue = redHealthBarImage.fillAmount;
        float endValue = greenHealthBarImage.fillAmount;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            redHealthBarImage.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }

        // Asegurarse de que fillAmount es exactamente 0.5 al final de la animación
        redHealthBarImage.fillAmount = endValue;
    }


}
