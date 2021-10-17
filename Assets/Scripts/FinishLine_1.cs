using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine_1 : MonoBehaviour
{
    public bool isTouched = false;

    MeshRenderer parentMesh;

    public Color green;
   
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PointBox"))
        {
            isTouched = true;
            parentMesh = gameObject.GetComponentInParent<MeshRenderer>(); 
            parentMesh.material.SetColor("_Color", Color.green);
            parentMesh.material.color = green;

        }
    }
}
