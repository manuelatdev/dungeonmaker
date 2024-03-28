using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptMovimientoHeroe : MonoBehaviour
{

    public GameObject exit;
    private NavMeshAgent agent;
    [SerializeField]
    private Queue<GameObject> colaEnemigos = new Queue<GameObject>();
    [SerializeField]
    private Queue<GameObject> colaCofres = new Queue<GameObject>();
    public GameObject targetActual;
    [SerializeField]
    private GameObject spriteGameobject;
    [HideInInspector]
    public bool mirandoIzquierda;
    private HeroScript scriptHero;
    public RangoAtaqueScript heroAttackScript;
    private Vector3 initialPosition;
    private int layerEnemy;
    

    private void Start()
    {
        layerEnemy = LayerMask.GetMask("Enemy");
        initialPosition = transform.position;
        heroAttackScript = GetComponentInChildren<RangoAtaqueScript>();
        scriptHero = GetComponent<HeroScript>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
        targetActual = exit;
    }


    private void Update()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (agent.velocity.x > 0f && mirandoIzquierda)
            {
                spriteGameobject.transform.rotation = new Quaternion(0, 0, 0, 1);
                mirandoIzquierda = false;

            }
            else if (agent.velocity.x < 0f && !mirandoIzquierda)
            {
                spriteGameobject.transform.rotation = new Quaternion(0, 1, 0, 0);
                mirandoIzquierda = true;

            }
            if (heroAttackScript.atacando && !agent.isStopped)
            {
                agent.isStopped = true;
            }
            else if (!heroAttackScript.atacando && agent.isStopped && heroAttackScript.animatorHero.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                agent.isStopped = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {


        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !colaEnemigos.Contains(collision.gameObject))
            {

                colaEnemigos.Enqueue(collision.gameObject); // 
                if (colaEnemigos.Count + colaCofres.Count < 2)
                {
                    SetNextDestination();
                }
                else if (targetActual?.layer == LayerMask.NameToLayer("Chest"))
                {
                    SetNextDestination();

                }


            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest") && !colaCofres.Contains(collision.gameObject))
            {

                colaCofres.Enqueue(collision.gameObject);
                if (colaEnemigos.Count + colaCofres.Count < 2)
                {
                    SetNextDestination();
                }



            } 
        }

    }

    public void OrganizeQueue()
    {
        List<GameObject> list = new List<GameObject>(colaEnemigos);
        colaEnemigos.Clear();

        list.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position)));

        foreach (GameObject obj in list)
        {
            colaEnemigos.Enqueue(obj);
        }
    }
    public void GoPlayMode()
    {
        DetectCollidersIn();
        OrganizeQueue();
        SetNextDestination();
        agent.isStopped = false;
        heroAttackScript.animatorHero.SetBool("Walk", true);
        
        
    }
    public void StopHero()
    {
        agent.isStopped = true;
        heroAttackScript.animatorHero.SetBool("Walk", false);
        heroAttackScript.animatorHero.SetTrigger("Stop");
    }
    public void GoStopMode()
    {
        agent.isStopped = true;
        spriteGameobject.transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = initialPosition;
        heroAttackScript.animatorHero.SetBool("Walk", false);
        heroAttackScript.animatorHero.SetTrigger("Stop");
        colaCofres.Clear();
        colaEnemigos.Clear();
        scriptHero.ResetCurrentStats();
        targetActual = exit;


    }
    private void DetectCollidersIn()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 4,layerEnemy);
        foreach (var hitCollider in hitColliders)
        {
                colaEnemigos.Enqueue(hitCollider.gameObject);
            
        }
    }
    public void NextTarget()
    {
        if (targetActual.layer == LayerMask.NameToLayer("Enemy"))
        {
             colaEnemigos.Dequeue();
        }
        else if (targetActual.layer == LayerMask.NameToLayer("Chest"))
        {
             colaCofres.Dequeue();


        }

        SetNextDestination();
    }
   
    
    public void AttackToEnemy()
    {
        targetActual?.GetComponent<BaseEntity>().TakeAttack(scriptHero.getDamage());
    }

    public void SetNextDestination()
    {

        if (colaEnemigos.Count > 0)
        {
            targetActual = colaEnemigos.Peek();
            agent.SetDestination(targetActual.transform.position);
        }
        else if (colaCofres.Count > 0)
        {
            targetActual = colaCofres.Peek();
            agent.SetDestination(targetActual.transform.position);
        }
        else
        {
            targetActual = exit;
            agent.SetDestination(exit.transform.position);
        }

    }


}
