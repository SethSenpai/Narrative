using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderMovement : MonoBehaviour {
    public float speed = 5f;
    public bool canClimb = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ladder")
        {
            canClimb = true;
            Debug.Log("hugging ladder");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            canClimb = false;
        }
        
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            //Debug.Log("MEEEEP");
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
        }

        if (Input.GetAxis("Vertical") < 1)
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
