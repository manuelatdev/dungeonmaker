using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BasicEnemy : BaseEntity
{
    public enum enemyType { slime, skeleton, archer, wizard,boss};

    public enemyType enemyClass;

    private TextMeshProUGUI healText;

    [SerializeField]
    private Image greenHealthBarImage;

    [SerializeField]
    private Image redHealthBarImage;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected int range;

    [SerializeField]
    protected float attackSpeed;

    [SerializeField]
    protected int cost;

    [SerializeField]
    protected int experience;

    private Image descriptionImg;

    private float mouseOverTime = 0;

    private bool descriptionOn = false;

    private ScriptTinteShader tinteScript;

    protected bool enemySelected;


    private Vector3 currentPosition;

    [SerializeField]
    private SpriteRenderer[] renderersUpables;

    [SerializeField]
    private GameObject detectionArea;

    [SerializeField]
    private GameObject[] outlines;

    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private Animator spriteAnim;
    [SerializeField]
    private Animator numbersAnim;
    [SerializeField]
    private TextMeshProUGUI damage1Text;
    [SerializeField]
    private TextMeshProUGUI damage2Text;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI expText;
    private bool damageNumAnim;
    public static int wizardsInGame;


    private bool charmed;

    [SerializeField]
    private Color colorBarCharmed;
    [SerializeField]
    private Color colorBarNormal;
    [SerializeField]
    private ParticleSystem charmedParticles;
    private ScriptFlecha[] flechasScript;
    private bool imDead;
    private bool subido;
    private bool girado;
    [SerializeField]
    private GameObject spriteObject;
    public override void Start()
    {
        base.Start();
        if (enemyClass == enemyType.archer)
        {
            flechasScript = GetComponentsInChildren<ScriptFlecha>(true);
        }
        wizardsInGame = 0;

        tinteScript = GetComponentInChildren<ScriptTinteShader>();
        healText = GetComponentInChildren<TextMeshProUGUI>();
        totalHealth = health;
        healText.text = health + " / " + totalHealth;
        SetAudioRef();
    }
    private void Update()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (scriptHero.transform.position.y > transform.position.y && !subido)
            {
                SpriteLayerUp();
                subido = true;
            }
            else if (scriptHero.transform.position.y < transform.position.y && subido)
            {
                SpriteLayerDown();
                subido = false;
            }
            if (scriptHero.transform.position.x > transform.position.x && !girado)
            {
                spriteObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                girado = true;
            }
            else if (scriptHero.transform.position.x < transform.position.x && girado)
            {
                spriteObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                girado = false;
            }
        }
    }

    public void GoCharm()
    {
        wizardsInGame ++;
        // Obtén todos los scripts BasicEnemy en la escena
        BasicEnemy[] enemies = GameObject.FindObjectsOfType<BasicEnemy>();

        // Itera sobre cada script BasicEnemy
        foreach (BasicEnemy enemy in enemies)
        {
            // Comprueba si el script BasicEnemy no es el que está usando esta función
            if (enemy != this)
            {
                // Activa la función GetCharmed
                enemy.GetCharmed();
            }
        }
    }
    public void GoDesCharm()
    {
        wizardsInGame --;
        // Obtén todos los scripts BasicEnemy en la escena
        BasicEnemy[] enemies = GameObject.FindObjectsOfType<BasicEnemy>();

        // Itera sobre cada script BasicEnemy
        foreach (BasicEnemy enemy in enemies)
        {
            // Comprueba si el script BasicEnemy no es el que está usando esta función
            if (enemy != this)
            {
                // Activa la función GetCharmed
                enemy.GetDesCharmed();
            }
        }
    }
    public void ActualizarBarra()
    {
        redHealthBarImage.fillAmount = (float)health/totalHealth;
        greenHealthBarImage.fillAmount = (float)health / totalHealth;
        healText.text = health + " / " + totalHealth;
    }
    public void GetCharmed()
    {
        charmed = true;
        greenHealthBarImage.color = colorBarCharmed;
        damage *= 2;
        health *= 2;
        experience *= 2;
        gold *= 2;
        totalHealth *= 2;
        charmedParticles.gameObject.SetActive(true);
        charmedParticles.Play();
        ActualizarBarra();
            
    }
    public void GetTotalCharmed()
    {
        int multipilcador = Mathf.Max((2 * wizardsInGame), 1);
        charmed = true;
        greenHealthBarImage.color = colorBarCharmed;
        damage *= multipilcador;
        health *= multipilcador;
        experience *= multipilcador;
        gold *= multipilcador;
        totalHealth *= multipilcador;
        charmedParticles.gameObject.SetActive(true);
        charmedParticles.Play();
        ActualizarBarra();

    }
    public void GetDesCharmed()
    {
        if (wizardsInGame==0)
        {
            charmed = false;
            greenHealthBarImage.color = colorBarNormal;
            charmedParticles.gameObject.SetActive(false);
            
        }
        else if (enemyClass == enemyType.wizard&&wizardsInGame == 1)
        {
            charmed = false;
            greenHealthBarImage.color = colorBarNormal;
            charmedParticles.gameObject.SetActive(false);
            
        }
        else
        {
            charmed = true;
            greenHealthBarImage.color = colorBarCharmed;
            charmedParticles.gameObject.SetActive(true);
            //charmedParticles.Play();
        }

        damage /= 2;
        totalHealth /= 2;
        health = Mathf.Min(health, totalHealth);
        experience /= 2;
        gold /= 2;
        ActualizarBarra();



    }
    public void GetTotalDesCharmed()
    {
        int multipilcador = Mathf.Max((2 * wizardsInGame), 1);
        charmed = false;
        greenHealthBarImage.color = colorBarNormal;
        damage /= multipilcador;
        health /= multipilcador;
        experience /= multipilcador;
        gold /= multipilcador;
        totalHealth /= multipilcador;
        charmedParticles.gameObject.SetActive(false);
        ActualizarBarra();

    }
    private void SetAudioRef()
    {
        switch (enemyClass)
        {
            case enemyType.slime:
                dieSound = AudioManagerScript.slimeDead;
                break;
            case enemyType.skeleton:
                dieSound = AudioManagerScript.skullDead;

                break;
            case enemyType.archer:
                dieSound = AudioManagerScript.archerDead;

                break;
            case enemyType.wizard:
                dieSound = AudioManagerScript.wizardDead;

                break;
           
        }
    }
    public void SetSelected(bool selected)
    {
        enemySelected = selected;
    }
    public void ActualizarCurrentPosition()
    {
        currentPosition = transform.position;
    }
    public void SpriteLayerUp()
    {
        foreach (SpriteRenderer render in renderersUpables)
        {
            render.sortingOrder += 20;
        }
    }
    public void SpriteLayerDown()
    {
        foreach (SpriteRenderer render in renderersUpables)
        {
            render.sortingOrder -= 20;
        }
    }
    public int getExperience()
    {
        return experience;
    }
    public int getDamage()
    {
        return damage;
    }
    private void OnMouseOver()
    {
        if (ScriptGameManager.gameMode != ModoJuego.Menu)
        {
            if (!SelectorScript.movingObject)
            {
                ActivateOutline(true);
                CursorScript.SwitchStone(true);
            }
            if (!descriptionOn && !enemySelected)
            {
                mouseOverTime += Time.unscaledDeltaTime;
                if (mouseOverTime > 0.5f)
                {
                    if (charmed)
                    {
                        
                            int damageBase = damage / Mathf.Max(wizardsInGame*2,1);
                            int experienceBase = experience / Mathf.Max(wizardsInGame * 2, 1);
                            int goldBase = gold / Mathf.Max(wizardsInGame * 2, 1);
                           string wizardBonus = (wizardsInGame > 1) ? "++" : "+";
                        if (enemyClass == enemyType.wizard)
                        {
                            wizardBonus = ((wizardsInGame - 1) > 1) ? "++" : "+";
                            damageBase = damage / Mathf.Max((wizardsInGame-1) * 2, 1);
                            experienceBase = experience / Mathf.Max((wizardsInGame - 1) * 2, 1);
                            goldBase = gold / Mathf.Max((wizardsInGame - 1) * 2, 1);
                        }
                            DesplegablesScript.ShowEnemyPlusDescription($"{damageBase}<color=#6a17b3>+{damage-damageBase}</color>", attackSpeed.ToString(), $"{experienceBase}<color=#6a17b3>+{experience - experienceBase}</color>", $"{goldBase}<color=#6a17b3>+{gold -goldBase}</color>", enemyClass.ToString().ToUpper() + wizardBonus);

                        
                       
                        
                    }
                    else
                    {
                        DesplegablesScript.ShowEnemyDescription(damage.ToString(), attackSpeed.ToString(), experience.ToString(), gold.ToString(), enemyClass.ToString().ToUpper());

                    }
                    descriptionOn = true;
                }
            } 
        }
    }
    public void ActivateOutline(bool active)
    {
        foreach(GameObject outline in outlines)
        {
            outline.SetActive(active);
                   }
        detectionArea.SetActive(active);

    }
    private void OnMouseExit()
    {
        if (!SelectorScript.movingObject)
        {
            ActivateOutline(false);
            CursorScript.SwitchStone(false);
        }
        if (descriptionOn)
        {
            DesplegablesScript.HideAllDescriptions();
        }
        descriptionOn = false;
        mouseOverTime = 0;



    }
    private void OnMouseDown()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit&&enemyClass != enemyType.boss)
        {
            AudioManagerScript.cardTaken.Play();
            if (enemyClass == enemyType.wizard)
            {
                GoDesCharm();
            }
           
            enemySelected = true;
            SelectorScript.movingObject = true;
            ActivateOutline(true);
            SpriteLayerUp();
            DesplegablesScript.HideAllDescriptions();
            TrashScript.ShowTrash(true);

        }
    }
    private void OnMouseDrag()
    {
        if (enemySelected && ScriptGameManager.gameMode == ModoJuego.Edit && enemyClass != enemyType.boss)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

        }
    }
    private void OnMouseUp()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit && enemyClass != enemyType.boss)
        {
            if (IsCreable())
            {
                AudioManagerScript.cardPlaced.Play();
                
                currentPosition = transform.position;
                if (enemyClass == enemyType.wizard)
                {
                    GoCharm();
                    AudioManagerScript.wizardSpell.Play();

                }

            }
            else if (TrashScript.mouseOnTrash)
            {
                dieSound.pitch = Random.Range(1f, 1.2f);
                dieSound.Play();
                EnergyScript.UseEnergy(cost);
                Destroy(this.gameObject);
            }
            else
            {
                AudioManagerScript.cardPlaced.Play();

                transform.position = currentPosition;
            }

            ActivateOutline(false);
            if (enemySelected)
            {
                SpriteLayerDown();
                enemySelected = false;

            }

            CursorScript.SwitchDenied(false);
            
            SelectorScript.movingObject = false;
            descriptionOn = false;
            mouseOverTime = 0;
            TrashScript.ShowTrash(false);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (enemySelected && !CursorScript.denied.activeSelf && !collision.gameObject.CompareTag("Hero"))
        {
            if (CursorScript.denied != null)
            {
                CursorScript.SwitchDenied(true);
            }
            else
            {
                StartCoroutine(WaitForSwitch(true));
            }
        }
    }
    IEnumerator WaitForSwitch(bool boleano)
    {
        yield return new WaitForEndOfFrame();
        CursorScript.SwitchDenied(boleano);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemySelected && !collision.gameObject.CompareTag("Hero"))
        {
            if (CursorScript.denied != null)
            {
                CursorScript.SwitchDenied(false);
            }
            else
            {
                StartCoroutine(WaitForSwitch(false));
            }

        }
    }

    public bool IsCreable()
    {
        if (!CursorScript.denied.activeSelf)
        {
            return true;
        }

        return false;
    }

    private void NumbersAnimation()
    {
        if (damageNumAnim)
        {
            if (scriptHero.movimientoScript.mirandoIzquierda)
            {
                numbersAnim.SetTrigger("DamageIzq");

            }
            else
            {
                numbersAnim.SetTrigger("Damage");

            }
            damage1Text.text = scriptHero.getDamage().ToString();
            damageNumAnim = false;
        }
        else
        {
            if (scriptHero.movimientoScript.mirandoIzquierda)
            {
                numbersAnim.SetTrigger("Damage2Izq");

            }
            else
            {
                numbersAnim.SetTrigger("Damage2");

            }
            damage2Text.text = scriptHero.getDamage().ToString();
            damageNumAnim = true;
        }
    }
    public override void TakeAttack(int damage)
    {
        base.TakeAttack(damage);

        if (health > 0)
        {

            NumbersAnimation();
            tinteScript.TintColor();
            greenHealthBarImage.fillAmount -= (float)damage / totalHealth;
            healText.text = health + " / " + totalHealth;
            StopCoroutine(AnimateHealthBarDecrease());
            StartCoroutine(AnimateHealthBarDecrease());
        }
        else
        {
            NumbersAnimation();

        }
    }
    public override void ResetEntity()
    {
        base.ResetEntity();
        if (wizardsInGame<1)
        {
            charmedParticles.gameObject.SetActive(false);
            
        }
        if (enemyClass == enemyType.wizard&&imDead)
        {
            GoCharm();
        }
        if (enemyClass == enemyType.archer)
        {
            foreach (ScriptFlecha script in flechasScript)
            {
                script.ResetArrow();
            }

        }
        if (subido)
        {
            SpriteLayerDown();
            subido = false;
        }
        if (girado)
        {
            spriteObject.transform.rotation = Quaternion.Euler(0,0,0);
            
            girado = false;
        }
        redHealthBarImage.fillAmount = 1;
        greenHealthBarImage.fillAmount = 1;
        healText.text = health + " / " + totalHealth;
        imDead = false;
    }
    protected override void Die()
    {
        dieSound.pitch = Random.Range(1f, 1.2f);
        dieSound.Play();
        scriptHero.OnEnemyDied(this, this);
        spriteAnim.SetTrigger("Reset");
        numbersAnim.SetTrigger("Dead");
        goldText.text = "+"+gold;
        expText.text = "+" + experience;
        imDead = true;
        if (enemyClass == enemyType.wizard)
        {
            GoDesCharm();
        }

        foreach (GameObject obj in disabledOnDead)
        {
            obj.SetActive(false);
        }
        meCollider.enabled = false;
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
            yield return null;
        }

        // Asegurarse de que fillAmount es exactamente 0.5 al final de la animación
        redHealthBarImage.fillAmount = endValue;
    }





}
