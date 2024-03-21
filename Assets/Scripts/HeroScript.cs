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
    private int heroDamage;

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

    private int initialActualHealth;

    private int initialTotalHealth;

    private int initialHeroLevel;

    private int initialHeroExp;

    private int initialHeroGold;

    private int initialHeroDamage;

    // UI
    [SerializeField]
    private TextMeshProUGUI goldLabel;

    [SerializeField]
    private TextMeshProUGUI attackLabel;


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

    [SerializeField]
    private Animator redScreenAnim;

    private ScriptTinteShader tinteScript;

    private AudioSource deadSound;

    private ScriptCamera cameraScript;

    [SerializeField]
    private AudioSource levelUpSound;

    [SerializeField]
    private Animator damageAnimator;

    private bool damageAnimActive;

    [SerializeField]
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI damageText2;
    public int getDamage()
    {
        return heroDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = Camera.main.GetComponent<ScriptCamera>();
        tinteScript = GetComponentInChildren<ScriptTinteShader>();
        deadSound = GetComponent<AudioSource>();
        movimientoScript = GetComponent<ScriptMovimientoHeroe>();
        heroLevel = 1;
        levelLabel.text = heroLevel.ToString();
        ActualizarMarcador();
        actualHealth = totalHealth;
        SetInitialStats();
    }
    private void SetInitialStats()
    {
        initialActualHealth = actualHealth;
        initialTotalHealth = totalHealth;
        initialHeroLevel = heroLevel;
        initialHeroExp = heroExperience;
        initialHeroGold = heroGold;
        initialHeroDamage = heroDamage;

    }

    public void ResetCurrentStats()
    {
        actualHealth = initialActualHealth;
        totalHealth = initialTotalHealth;
        heroLevel = initialHeroLevel;
        heroExperience = initialHeroExp;
        heroGold = initialHeroGold;
        heroDamage = initialHeroDamage;
        healText.text = actualHealth + " / " + totalHealth;
        redHealthBarImage.fillAmount = 1;
        greenHealthBarImage.fillAmount = 1;
        ActualizarMarcador();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnemyDied(BasicEnemy enemyScript,BaseEntity entityScript)
    {
        

        heroGold += entityScript.getGold();
        if (enemyScript != null)
        {
            heroExperience += enemyScript.getExperience(); 
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
        damageAnimator.SetTrigger("LevelUp");
        levelUpSound.Play();
        heroExperience -= experienceLevels[heroLevel];
        heroLevel++;
        heroDamage++;
        actualHealth += 5;
        totalHealth += 5;
        StopAllCoroutines();
        greenHealthBarImage.fillAmount = (float)actualHealth / totalHealth;
        redHealthBarImage.fillAmount = (float)actualHealth / totalHealth;
        healText.text = actualHealth + " / " + totalHealth;

    }
    private void ActualizarMarcador()
    {
        levelLabel.text = heroLevel.ToString();
        experienceBar.fillAmount = (float)((float)heroExperience / (float)experienceLevels[heroLevel]);
        goldLabel.text = "x"+heroGold.ToString();
        attackLabel.text = "x" + heroDamage.ToString();
        defLabel.text = "x" + def.ToString();
        speedLabel.text = "x" + attackSpeed.ToString();
        experienceLabel.text = heroExperience + "/" + experienceLevels[heroLevel];

    }
    public  void TakeAttack(int damage)
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (damageAnimActive)
            {
                damageAnimator.SetTrigger("Damage");
                damageText.text = damage.ToString();
            }
            else
            {
                damageAnimator.SetTrigger("Damage2");
                damageText2.text = damage.ToString();


            }
            tinteScript.TintColor();
            bloodParticles.Play();
            greenHealthBarImage.fillAmount -= (float)damage / totalHealth;
            actualHealth -= damage;
            if (actualHealth < 1) 
            {
                ScriptGameManager.gameMode = ModoJuego.Menu;
                actualHealth = 0;
                Time.timeScale = 0.2f;
                movimientoScript.heroAttackScript.animatorHero.SetTrigger("Dead");
                deadSound.Play();
                AudioManagerScript.music.Stop();
                cameraScript.DeadCamera();
                redScreenAnim.SetTrigger("DeadScreen");

                //muerteScreen
            }
            healText.text = actualHealth + " / " + totalHealth;
            StopCoroutine(AnimateHealthBarDecrease());
            StartCoroutine(AnimateHealthBarDecrease());
            redScreenAnim.SetTrigger("ScreenHit");
        }
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
