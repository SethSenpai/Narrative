using UnityEngine;
using System.Collections;

public class clickTrigger : MonoBehaviour {

    public bool clicked = false;
    private bool hover = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && hover == true)
        { 
            clicked = true;
        }
        else
        {
            clicked = false;
        }

    }

    void OnMouseEnter()
    {
        hover = true;
    }

    void OnMouseExit()
    {
        hover = false;
    }
}
