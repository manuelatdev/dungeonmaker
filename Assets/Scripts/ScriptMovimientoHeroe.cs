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
    private RangoAtaqueScript heroAttackScript;
    private Vector3 initialPosition;


    private void Start()
    {
        initialPosition = transform.position;
        heroAttackScript = GetComponentInChildren<RangoAtaqueScript>();
        scriptHero = GetComponent<HeroScript>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;

    }


    private void Update()
    {
        if (ScriptGameManager.gameMode == ModoJuego.Play)
        {
            if (agent.velocity.x > 0 && mirandoIzquierda)
            {
                spriteGameobject.transform.rotation = new Quaternion(0, 0, 0, 1);
                mirandoIzquierda = false;

            }
            else if (agent.velocity.x < 0 && !mirandoIzquierda)
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


        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !colaEnemigos.Contains(collision.gameObject))
        {
            collision.gameObject.GetComponent<BasicEnemy>().OnDie += scriptHero.OnEnemyDied;

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
            collision.gameObject.GetComponent<BaseEntity>().OnDie += scriptHero.OnEnemyDied;

            colaCofres.Enqueue(collision.gameObject);
            if (colaEnemigos.Count + colaCofres.Count < 2)
            {
                SetNextDestination();
            }



        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ScriptGameManager.gameMode == ModoJuego.Edit)
        {

            // Si el objeto que sale de la zona de colisión está en la cola de enemigos, lo quitamos de la cola
            if (colaEnemigos.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<BasicEnemy>().OnDie -= scriptHero.OnEnemyDied;

                Queue<GameObject> newQueue = new Queue<GameObject>();
                while (colaEnemigos.Count > 0)
                {
                    GameObject currentObject = colaEnemigos.Dequeue();
                    if (currentObject != collision.gameObject)
                    {
                        newQueue.Enqueue(currentObject);
                    }
                }
                colaEnemigos = newQueue;

                Debug.Log(colaEnemigos.Count.ToString());
            }

            // Si el objeto que sale de la zona de colisión está en la cola de cofres, lo quitamos de la cola
            if (colaCofres.Contains(collision.gameObject))
            {
                collision.gameObject.GetComponent<BasicEnemy>().OnDie -= scriptHero.OnEnemyDied;

                Queue<GameObject> newQueue = new Queue<GameObject>();
                while (colaCofres.Count > 0)
                {
                    GameObject currentObject = colaCofres.Dequeue();
                    if (currentObject != collision.gameObject)
                    {
                        newQueue.Enqueue(currentObject);
                    }
                }
                colaCofres = newQueue;

                Debug.Log(colaCofres.Count.ToString());
            }
            targetActual = null;
        }
    }

    public void GoPlayMode()
    {
        SetNextDestination();
        agent.isStopped = false;
        heroAttackScript.animatorHero.SetBool("Walk", true);

    }
    public void GoStopMode()
    {
        agent.isStopped = true;
        colaCofres.Clear();
        colaEnemigos.Clear();
        spriteGameobject.transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = initialPosition;
        heroAttackScript.animatorHero.SetTrigger("Stop");

        heroAttackScript.animatorHero.SetBool("Walk", false);

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
