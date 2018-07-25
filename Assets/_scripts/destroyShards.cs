using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyShards : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1)
        {
            Debug.Log("destroy shard");
            Destroy(gameObject);
        }
	}
}
