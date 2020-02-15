using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {


    public float moveSpeed = 40;
   // Vector3 vector = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(transform.position.y<=300)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);         
        }
	}
}
