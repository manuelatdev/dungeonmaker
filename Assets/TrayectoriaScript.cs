using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayectoriaScript : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField]
    private ScriptMovimientoHeroe heroMoveScript;
    // Start is called before the first frame update
    void Start()
    {
     line = GetComponent<LineRenderer>();   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPositionInLocalSpace = heroMoveScript.transform.InverseTransformPoint(heroMoveScript.targetActual.transform.position);
        line.SetPosition(1, targetPositionInLocalSpace);
    }
}
