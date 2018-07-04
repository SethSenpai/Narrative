using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSync : MonoBehaviour {

    public GameObject setLight;

	// Use this for initialization
	void Start () {
        setLight.GetComponent<Light>().color = this.GetComponent<Light>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
