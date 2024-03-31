using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptFlecha : MonoBehaviour
{
    private Transform objetivo; // El objeto al que quieres dirigirte
    [SerializeField]
    private float velocidad = 5.0f; // La velocidad a la que quieres moverte
    private EnemyAnimatorScript enemyScript;
    private bool stop;
    private float tiempoVida;
    [SerializeField]
    private float tiempoMaximo;
    private Collider2D colliderArrow;

  
    private void Awake()
    {
        colliderArrow = GetComponent<Collider2D>();
        enemyScript = GetComponentInParent<EnemyAnimatorScript>();
        objetivo = FindAnyObjectByType<HeroScript>().gameObject.transform;
    }
    private void OnEnable()
    {
        stop = false;
        tiempoVida = 0;

        Vector2 direccion = objetivo.position - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        colliderArrow.enabled = true;
    }
    void Update()
    {
        if (!stop)
        {
            // Calcula la dirección hacia el objetivo
            Vector2 direccion = objetivo.position - transform.position;

            // Normaliza la dirección (para que su longitud sea 1) y multiplica por la velocidad y el tiempo del frame
            Vector2 movimiento = direccion.normalized * velocidad * Time.deltaTime;

            // Mueve el objeto hacia el objetivo, pero no más allá de este
            if (movimiento.magnitude < direccion.magnitude)
            {
                transform.position += (Vector3)movimiento;
            }
            else
            {
                transform.position = objetivo.position;
            }

            // Hace que el objeto mire al objetivo
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo); 
        }

        tiempoVida += Time.unscaledDeltaTime;
        if (tiempoVida > tiempoMaximo)
        {
            tiempoVida = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HeroBody"))
        {
            enemyScript.AttackToHero();
            gameObject.SetActive(false);
            colliderArrow.enabled = false;

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            stop = true;
            AudioManagerScript.archerHitWall.Play();
            colliderArrow.enabled = false;

        }
    }
    public void ResetArrow()
    {
        gameObject.SetActive(false);
        if (colliderArrow != null)
        {
            colliderArrow.enabled = false;
        }
    }
}
