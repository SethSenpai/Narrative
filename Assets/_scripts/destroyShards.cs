using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyShards : MonoBehaviour {

    private Vector3 oldposition;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        foreach (Transform child in transform)
        {
            if (float.IsNaN(child.position.x) || float.IsNaN(child.position.y) || float.IsNaN(child.position.z))
            {
                Destroy(child.gameObject);
                //Debug.Log("isNaN");
            }
        }
    }

}
