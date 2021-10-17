using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Color myColor;
    [SerializeField] Renderer[] myRends;
    [SerializeField] bool isPlaying;
    [SerializeField] float forwardSpeed;
    [SerializeField] float tempForwardSpeed;


    [SerializeField] float sideLerpSpeed;

    [SerializeField] Transform parentPickup;
    [SerializeField] Transform stackListTransform;
    [SerializeField] Transform stackTransform;
    [SerializeField] Transform playerModel;
    [SerializeField] Rigidbody playerRB;

    [SerializeField] int cubeCounter;

    [SerializeField] public Transform objTransform;

    [SerializeField] GameObject cardboardBox;

    [SerializeField] int numChildren;

    bool gotChild = false;


   public List<GameObject> stackList = new List<GameObject>();


    public Rigidbody tempObjRb;
    public BoxCollider tempObjBC;

    public float trust = 30;


    CameraController CameraController;

    private CameraMovement CameraMovement;

    float xMax = 3.5f;
    float xMin = -3.5f;
    Vector3 targetPos;
    private void Awake()
    {
        
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        SetColor(myColor);

        cubeCounter = 0;

        CameraMovement = GameObject.FindObjectOfType(typeof(CameraMovement)) as CameraMovement;

        tempForwardSpeed = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            

            MoveForward();
        }
        if (Input.GetMouseButton(0))
        {
            forwardSpeed = tempForwardSpeed;
            if (isPlaying == false)
            {
                isPlaying = true;
            }
            MoveSideWays();
        }
        else if (!Input.GetMouseButton(0))
        {
            forwardSpeed = 0;
        }
    }
    

    void SetColor(Color colorIn)
    {
        myColor = colorIn;
        for(int i = 0; i< myRends.Length; i++)
        {
            myRends[i].material.SetColor("_Color", myColor);
        }
    }
    void MoveForward()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;

        //playerRB.velocity = Vector3.forward * forwardSpeed;
    }
    void StopMoving()
    {
        forwardSpeed = 0;
    }

    void MoveSideWays()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, 100))
        {
            targetPos.x = hit.point.x;
            if (hit.point.x > xMax)
            {
                targetPos.x = xMax;
            }
            else if (hit.point.x < xMin)
            {
                targetPos.x = xMin;
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x,
                                                                              transform.position.y,
                                                                              transform.position.z), sideLerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Pickup")
        {

            CameraMovement.IncreaseCameraAngle();


            Debug.Log("küp aldý");

            obj.gameObject.tag = "Untagged";

            if (stackListTransform.childCount <= 0)
            {
                gotChild = false;
            }
            else if (stackListTransform.childCount > 0)
            {
                gotChild = true;

            }
            if (gotChild == false)
            {
                //GameObject myCube = Instantiate(cardboardBox, stackTransform.position, Quaternion.identity) as GameObject;
                //Debug.Log("null");
                ////playerModel.position += Vector3.up * (cardboardBox.transform.localScale.y);//playermodeli 1 küb yukarý al
                //myCube.transform.parent = stackListTransform;
                //stackList.Add(myCube);


                obj.GetComponent<Collider>().enabled = false;

                obj.transform.parent = stackTransform;
                obj.transform.position = stackListTransform.position;
                stackList.Add(obj.gameObject);
                stackListTransform.position += Vector3.up * (cardboardBox.transform.localScale.y);

            }
            else if (gotChild == true)
            {
                //GameObject myCube = Instantiate(cardboardBox, stackTransform.position, Quaternion.identity) as GameObject;
                //Debug.Log("NOT null");
                ////playerModel.position += Vector3.up * (cardboardBox.transform.localScale.y);//playermodeli 1 küb yukarý al
                //stackListTransform.position += Vector3.up * (cardboardBox.transform.localScale.y);
                //myCube.transform.parent = stackListTransform;
                //stackList.Add(myCube);


                stackListTransform.position += Vector3.up * (cardboardBox.transform.localScale.y);
                obj.transform.parent = stackTransform;
                obj.transform.position = stackListTransform.position;
                stackList.Add(obj.gameObject);



            }
            else
            {

            }
            //Destroy(obj.gameObject);
            return;
            


        }
        else if (obj.CompareTag("Obstacle_Easy") || obj.CompareTag("Obstacle_Medium") || obj.CompareTag("Obstacle_Hard"))
        {
            int tempRandomNumber = obj.gameObject.GetComponent<Obstacle>().damage;

            numChildren = stackListTransform.transform.childCount;
            Debug.Log("TRIGGERED");

            if (stackList.Count-tempRandomNumber >= 0)
            {
                Debug.Log(tempRandomNumber);

                for (int i = tempRandomNumber; i > 0; i--)
                {
                    Debug.Log("decrease cube count");
                    GameObject tempObj = stackList[stackList.Count - 1];
                   
                    tempObjRb = tempObj.GetComponent<Rigidbody>();
                    // tempObjBc = tempObj.GetComponent<BoxCollider>();
                    tempObjBC = tempObj.GetComponent<BoxCollider>();

                    tempObjRb.isKinematic = false;
                    tempObjRb.useGravity = true;
                    tempObj.transform.parent = null;
                    tempObjRb.freezeRotation = false;
                    tempObjBC.enabled = false;

                    tempObjRb.AddForce(((transform.forward * (-20)) + (transform.up)) * trust);



                    Destroy(obj.gameObject);

                    //tempObjBc.isTrigger = true;

                    stackList.RemoveAt(stackList.Count - 1);
                    //Destroy(tempObj);
                    //playerModel.position += Vector3.down * (cardboardBox.transform.localScale.y);//playermodeli 1 küb yukarý al
                    stackListTransform.position += Vector3.down * (cardboardBox.transform.localScale.y);
                    //Destroy(obj.gameObject);

                    CameraMovement.DecreaseCameraAngle();



                }

            }
            
            
            

        }
        else if (obj.CompareTag("FinishLine"))
        {
            float tempListCount = stackList.Count;
            isPlaying = false;
            Debug.Log(isPlaying);

            for (int i = 0; i <= tempListCount-1; i++)
            {
                Debug.Log("KÜP ATILDI");
                Debug.Log(tempListCount-1);
                Debug.Log("Ý ="+ i);



                GameObject tempPickup = stackList[stackList.Count - 1];

                tempPickup.gameObject.tag = "PointBox";

                tempPickup.GetComponent<Collider>().enabled = true;
                obj.GetComponent<Collider>().enabled = true;

                Rigidbody tempPickupRb = tempPickup.GetComponent<Rigidbody>();

                tempPickupRb.isKinematic = false;
                tempPickupRb.useGravity = true;
                tempPickup.transform.parent = null;

                tempPickupRb.freezeRotation = false;

                tempPickupRb.AddForce(((transform.forward * 20) + (transform.up)) * trust );


                stackList.RemoveAt(stackList.Count - 1);
            }
            StartCoroutine(ExampleCoroutine());
        }
        //else
        //{
        //    Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);//light shader buglanýyor level restart olunca
        //                                                                                    //ölme scripti gelicek
        //}

        else if (obj.CompareTag("PathBox"))
        {
            if (stackList.Count > 0)
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");

                GameObject tempObj = stackList[stackList.Count - 1];
                Destroy(tempObj.gameObject);

                obj.GetComponent<Renderer>().enabled = true;

                stackList.RemoveAt(stackList.Count - 1);

                stackListTransform.position += Vector3.down * (cardboardBox.transform.localScale.y);

                CameraMovement.DecreaseCameraAngle();
            }
            else if (stackList.Count <= 0)
            {
                Debug.Log("BBBBBBBBBBBBBBBBBBBBBB");

                obj.GetComponent<Collider>().enabled = false;

            }

        }
    }
    IEnumerator ExampleCoroutine()
    {


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return null;
    }
}
     
    


