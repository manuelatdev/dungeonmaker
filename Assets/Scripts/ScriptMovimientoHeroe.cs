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
    private void Start()
    {
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
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")&& !colaEnemigos.Contains(collision.gameObject))
        {
            colaEnemigos.Enqueue(collision.gameObject);
            if (colaEnemigos.Count + colaCofres.Count < 2)
            {
                SetNextDestination();
            }
            print("añadido " + collision.gameObject.name);
            
            
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Chest")&& !colaCofres.Contains(collision.gameObject))
        {
            colaCofres.Enqueue(collision.gameObject);
            if (colaEnemigos.Count + colaCofres.Count < 2)
            {
                SetNextDestination();
            }
            print("añadido " + collision.gameObject.name);



        }
    }
    
    public void AttackToEnemy()
    {



        targetActual.GetComponent<BasicEnemy>().InflictDamage(GetComponentInParent<HeroScript>().getDamage());

        if(targetActual.GetComponent<BasicEnemy>().getHeath() <=0)
        {
            if (targetActual.layer == LayerMask.NameToLayer("Enemy"))
            {
                colaEnemigos.Dequeue();
                print("eliminado " + targetActual.name);
            }
            else if (targetActual.layer == LayerMask.NameToLayer("Chest"))
            {
                colaCofres.Dequeue();
                print("eliminado " + targetActual.name);


            }

            targetActual = exit;
            SetNextDestination();
        }


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
