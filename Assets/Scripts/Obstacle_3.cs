using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_3 : MonoBehaviour
{
    public Vector3 posB;
    public Vector3 posA;

    public bool hasArrived = false;

    [SerializeField]
    private float speed = 0.1f;
    void Start()
    {
        posA = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        posB = new Vector3 (transform.position.x, transform.position.y - 0.6f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {

        if (hasArrived == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, posB, speed * Time.deltaTime);
            if (transform.position == posB)
            {
                hasArrived = true;
            }
        }
        else if (hasArrived == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, posA, speed * Time.deltaTime);
            if (transform.position == posA)
            {
                hasArrived = false;
            }
        }       
       
    }
}
