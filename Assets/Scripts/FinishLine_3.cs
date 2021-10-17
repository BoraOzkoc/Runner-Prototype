using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine_3 : MonoBehaviour
{
    public bool isTouched = false;

    MeshRenderer parentMesh;

    public Color orange; 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PointBox"))
        {
            isTouched = true;
            parentMesh = gameObject.GetComponentInParent<MeshRenderer>();
            parentMesh.material.SetColor("_Color", Color.blue);
            parentMesh.material.color = orange;

        }
    }
}
