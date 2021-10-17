using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] public int damage;


    PlayerController playerController;

    public bool randomNumbered = true;

     
    void Start()
    {
        if (CompareTag("Obstacle_Easy"))
        {
            if (randomNumbered)
            {
                damage = Random.Range(1, 3);
                gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString("0"); 

            }

        }
        else if (CompareTag("Obstacle_Medium"))
        {
            if (randomNumbered)
            {
                damage = Random.Range(6, 9);
                gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString("0");

            }

        }
        else if (CompareTag("Obstacle_Hard"))
        {
            if (randomNumbered)
            {
                damage = Random.Range(9, 12);
                gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString("0");
            }

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
