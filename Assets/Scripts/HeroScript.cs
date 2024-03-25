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

    [HideInInspector]
    public int actualHealth;

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
    private Image greenHealthBarImagePanel;

    [SerializeField]
    private Image redHealthBarImagePanel;

    [SerializeField]
    private TextMeshProUGUI healText;

    [SerializeField]
    private TextMeshProUGUI healTextPanel;

    [SerializeField]
    private ParticleSystem bloodParticles;
    [HideInInspector]
    public ScriptMovimientoHeroe movimientoScript;

    [HideInInspector] 
    public int heroGold;

    [HideInInspector]
    public int expTotalObtenida;

    [HideInInspector]
    public int healthTotalRestada=0;

    private int heroExperience;

    [HideInInspector]
    public  int heroLevel;

    [HideInInspector]
    public int initialActualHealth;

    private int initialTotalHealth;

    [HideInInspector]
    public int initialHeroLevel;

    private int initialHeroExp;

    [HideInInspector]
    public int initialHeroGold;

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
        
        
        actualHealth = totalHealth;
        SetInitialStats();
        ActualizarMarcador();
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
        redHealthBarImage.fillAmount = (float)actualHealth / totalHealth; ;
        greenHealthBarImage.fillAmount = (float)actualHealth / totalHealth;
        healTextPanel.text = actualHealth + " / " + totalHealth;
        redHealthBarImagePanel.fillAmount = (float)actualHealth / totalHealth; ;
        greenHealthBarImagePanel.fillAmount = (float)actualHealth / totalHealth; ;
        expTotalObtenida = 0;
        healthTotalRestada = 0;

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
            expTotalObtenida += enemyScript.getExperience();
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

        greenHealthBarImagePanel.fillAmount = (float)actualHealth / totalHealth;
        redHealthBarImagePanel.fillAmount = (float)actualHealth / totalHealth;
        healTextPanel.text = actualHealth + " / " + totalHealth;

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
        healText.text = actualHealth + " / " + totalHealth;
        healTextPanel.text = actualHealth + " / " + totalHealth;

    }
    public  void TakeAttack(int damage)
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (damageAnimActive)
            {
                if (movimientoScript.mirandoIzquierda)
                {
                    damageAnimator.SetTrigger("DamageDere");
                }
                else
                {
                    damageAnimator.SetTrigger("Damage");
                }
                
                damageText.text = damage.ToString();
                damageAnimActive = false;
            }
            else
            {
                if (movimientoScript.mirandoIzquierda)
                {
                    damageAnimator.SetTrigger("Damage2Dere");

                }
                else
                {
                    damageAnimator.SetTrigger("Damage2");

                }
                damageText2.text = damage.ToString();
                damageAnimActive = true;


            }
            tinteScript.TintColor();
            bloodParticles.Play();
            greenHealthBarImage.fillAmount -= (float)damage / totalHealth;
            greenHealthBarImagePanel.fillAmount -= (float)damage / totalHealth;
            actualHealth -= damage;
            healthTotalRestada -= damage;
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
            healTextPanel.text = actualHealth + " / " + totalHealth;
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
        yield return new WaitForSeconds(0.1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            redHealthBarImage.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            redHealthBarImagePanel.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }

        // Asegurarse de que fillAmount es exactamente 0.5 al final de la animación
        redHealthBarImage.fillAmount = endValue;
        redHealthBarImagePanel.fillAmount = endValue;

    }


}
