using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class crouchPlayer : MonoBehaviour {
    public Camera playerCam;
    public float crouchHeight;
    private float oldHeight;
    private bool canStand = true;
    private CharacterController chc;
    private FirstPersonController fpsc;
    private float oldSpeed;
    public float crouchSpeed = 2;

    // Use this for initialization
    void Start () {
        chc = GetComponent<CharacterController>();
        fpsc = GetComponent<FirstPersonController>();
        oldHeight = chc.height;
        oldSpeed = fpsc.m_WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Crouch") > 0)
        {
            chc.height = crouchHeight;
            fpsc.m_WalkSpeed = crouchSpeed;
        }
        else if(Input.GetAxis("Crouch") < 1 && canStand == true)
        {
            chc.height = oldHeight;
            fpsc.m_WalkSpeed = oldSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "noStandZone")
        {
            canStand = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "noStandZone")
        {
            canStand = true;
        }

    }

}
