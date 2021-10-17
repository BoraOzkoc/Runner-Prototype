using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public Transform player;

    private float speed = 0.1f;
    //private Vector2 velSpeed;
    //Vector3 offset;
    private float defaultZoom = 5f;
    //private float zoomSpeed = 0.2f;
    private float velZoom;

    public float xMax = 1.8f;
    public float xMin = -1.8f;

    //private float height = 0;
    //private float yMin = 0;

    //public static CameraMovement Instance { get; set; }
    //// Start is called before the first frame update
    //void Start()
    //{
    //    Instance = this;
    //    offset = new Vector3(0, 0, -10f);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector2.SmoothDamp(current: transform.position, target: player.position + offset , currentVelocity: ref velSpeed, smoothTime: zoomSpeed, speed);

    //    float desiredZoom = defaultZoom + Vector2.Distance(a: transform.position, b: player.transform.position);
    //    if (desiredZoom > 18)
    //    {
    //        desiredZoom = 18;
    //    }
    //    Camera.main.orthographicSize =
    //        Mathf.SmoothDamp(current: Camera.main.orthographicSize, target: desiredZoom, currentVelocity: ref velZoom, speed);
    //    if (transform.position.y < yMin)
    //    {
    //        transform.position = new Vector3(transform.position.x, yMin, -10f);

    //    }

    //}

    GameObject playerobj;
    //public UnityEngine.Transform player;
    Transform player;
    [SerializeField]
    Vector3 offset;
    Vector3 targetPos;
    // Start is called before the first frame update

    Transform target;
    void Start()
    {
        playerobj = GameObject.FindGameObjectWithTag("Player");
        player = playerobj.transform;
        offset = transform.position - player.position;

        target = GameObject.FindGameObjectWithTag("Target").transform;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        targetPos = player.position;
        // Follow Transform
        //transform.position = player.position + offset;

        // Move Towards
        //float speed = 3.0f;
        //transform.position = Vector3.MoveTowards(transform.position, player.position + offset, Time.deltaTime * speed);

        // Linear Interpolation
        //float speed = 1.0f;
        //transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * speed);
        if (player.position.x > xMax)
        {
            targetPos.x = xMax;
        }
        else if (player.position.x < xMin)
        {
            targetPos.x = xMin;

        }
        // Smooth Damp
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos + offset, ref velocity, 0.09f);

        transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));

        //print(velocity);

        //float desiredZoom = defaultZoom + Vector2.Distance(a: transform.position, b: player.transform.position);
        //if (desiredZoom > 18)
        //{
        //    desiredZoom = 18;
        //}
        //Camera.main.orthographicSize =
        //    Mathf.SmoothDamp(current: Camera.main.orthographicSize, target: desiredZoom, currentVelocity: ref velZoom, speed);


    }
    public void IncreaseCameraAngle()
    {
        //Vector3 NewPos = new Vector3(this.transform.position.x, player.position.y , 0);
        //this.transform.position = Vector3.Lerp(transform.position, NewPos, Time.deltaTime);

        //offsetCamera.z = Vector3.Lerp(transform.position, NewPos, Time.deltaTime);

        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 1f);
        offset.z -= 0.3f;
        offset.y += 0.5f;


    }
    public void DecreaseCameraAngle()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 1f);
        offset.z += 0.3f;
        offset.y -= 0.5f;
    }
}