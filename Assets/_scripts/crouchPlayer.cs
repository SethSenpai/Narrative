using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchPlayer : MonoBehaviour {
    public Camera playerCam;
    public float crouchHeight;
    private float oldHeight;

    // Use this for initialization
    void Start () {
        oldHeight = this.GetComponent<CharacterController>().height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Crouch") > 0)
        {
            this.GetComponent<CharacterController>().height = crouchHeight;
        }
        else
        {
            this.GetComponent<CharacterController>().height = oldHeight;
        }
    }

}
