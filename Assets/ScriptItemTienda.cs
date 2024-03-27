using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptItemTienda : MonoBehaviour
{
    [SerializeField]
    private GameObject particulasBack;

  public void MoverParticulas()
    {

        Vector3 newPosition = new Vector3(transform.position.x, particulasBack.transform.position.y, particulasBack.transform.position.z);
        particulasBack.transform.position = newPosition;
    }
}
