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
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetNextDestination();

    }
    private void Update()
    {
        if (colaEnemigos.Any()) 
        {
            foreach (GameObject objeto in colaEnemigos)
            {
                print(objeto.name);
            } 
        }
        if (colaEnemigos.Any())
        {
            foreach (GameObject objeto in colaCofres)
            {
                print(objeto.name);
            }
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

    public void EnemigoMuerto()
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
        targetActual.GetComponent<SpriteRenderer>().enabled = false;
        targetActual = exit;
        SetNextDestination();
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
