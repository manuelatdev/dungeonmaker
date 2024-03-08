using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
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
    private bool mirandoIzquierda;
    private HeroScript scriptHero;
    private void Start()
    {
        scriptHero = GetComponent<HeroScript>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetNextDestination();

    }
   

    private void Update()
    {
        if (agent.velocity.x > 0&&mirandoIzquierda)
        {
            spriteGameobject.transform.rotation = new Quaternion(0, 0, 0, 1);
            mirandoIzquierda = false;

        }
        else if (agent.velocity.x < 0&&!mirandoIzquierda)
        {
            spriteGameobject.transform.rotation = new Quaternion(0, 1, 0, 0);
            mirandoIzquierda = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        BaseEntity objectCollisioned = collision.gameObject.GetComponent<BaseEntity>();

        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")&& !colaEnemigos.Contains(collision.gameObject))
        {
            collision.gameObject.GetComponent<BasicEnemy>().OnDie += scriptHero.OnEnemyDied;

            colaEnemigos.Enqueue(collision.gameObject);
            if (colaEnemigos.Count + colaCofres.Count < 2)
            {
                SetNextDestination();
            }
            else if (targetActual.layer == LayerMask.NameToLayer("Chest"))
            {
                SetNextDestination();

            }


        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest")&& !colaCofres.Contains(collision.gameObject))
        {
            collision.gameObject.GetComponent<BaseEntity>().OnDie += gameObject.GetComponent<HeroScript>().OnEnemyDied;

            colaCofres.Enqueue(collision.gameObject);
            if (colaEnemigos.Count + colaCofres.Count < 2)
            {
                SetNextDestination();
            }



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

        targetActual = exit;
        SetNextDestination();
    }
    

    public void AttackToEnemy()
    {
        targetActual.GetComponent<BaseEntity>().TakeAttack(GetComponentInParent<HeroScript>().getDamage());
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
