using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderMovement : MonoBehaviour {
    public float speed = 5f;
    public bool canClimb = false;
    private bool collide = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ladder")
        {
            //collide = true;
            canClimb = true;
            //Debug.Log("hugging ladder");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            //collide = false;
            canClimb = false;
        }
        
    }

    void Update()
    {
        /*
        int layerMask = 1 << 10;
        //layerMask = ~layerMask;
        //clicked to hold something new since we didnt hold anything previously
        if (collide)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Looking at ladder");
                canClimb = true;
            }
            else
            {
                Debug.Log("not looking at ladder");
                canClimb = false;
            }
        }
        */
    }

}
