using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour {

    public string inputButton = "Action";
    public float holdDistance = 1f;
    public float pickupDistance = 3f;
    public float pullPower = 5f;
    public bool holding = false;
    private Transform physicsObjectHolding;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Input.GetAxis(inputButton));
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        //clicked to hold something new since we didnt hold anything previously
		if(holding == false && Input.GetAxis(inputButton) > 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, pickupDistance, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Range Checks out");
                
                if(hit.transform.tag == "PhysicsObject")
                {
                    Debug.Log("Physics object");
                    holding = true;
                    physicsObjectHolding = hit.transform;
                    physicsObjectHolding.GetComponent<Rigidbody>().useGravity = false;
                    Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, holdDistance));
                    physicsObjectHolding.position = Vector3.MoveTowards(physicsObjectHolding.position, point, pullPower * Time.deltaTime);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

        }

        //holding something and holding down the mouse
        else if(holding == true && Input.GetAxis(inputButton) > 0)
        {
            Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, holdDistance));
            physicsObjectHolding.position = Vector3.MoveTowards(physicsObjectHolding.position, point, pullPower * Time.deltaTime);
        }

        if (holding == true && Input.GetAxis(inputButton) < 1)
        {
            Debug.Log("unhanded");
            holding = false;
            physicsObjectHolding.GetComponent<Rigidbody>().useGravity = true;
            physicsObjectHolding = null;
        }
	}


}
