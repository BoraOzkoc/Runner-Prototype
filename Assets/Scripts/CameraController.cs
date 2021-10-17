using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //[SerializeField] Transform target;

    Transform player;

    [SerializeField] public Vector3 offsetCamera;
    Transform target;

    private float smoothSpeed = 0.1f;
    void Start()
    {
        // z = transform.position.z - target.position.z;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Target").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
        //Vector3 cameraAngle = target.position;
        //cameraAngle.z = (player.position + offsetAngle).z;
        //transform.LookAt(cameraAngle);

        

    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.z = (player.position + offsetCamera).z;
        position.y = (player.position + offsetCamera).y;
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed);


        transform.LookAt(new Vector3(transform.position.x, target.position.y, target.position.z));

    }
    public void IncreaseCameraAngle()
    {
        //Vector3 NewPos = new Vector3(this.transform.position.x, player.position.y , 0);
        //this.transform.position = Vector3.Lerp(transform.position, NewPos, Time.deltaTime);

        //offsetCamera.z = Vector3.Lerp(transform.position, NewPos, Time.deltaTime);

        offsetCamera.z -= 1f;
        offsetCamera.y += 0.5f;

        
    }
    public void DecreaseCameraAngle()
    {
        offsetCamera.z += 1f;
        offsetCamera.y -= 0.5f;
    }
}
