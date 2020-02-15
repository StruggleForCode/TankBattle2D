using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Shovel : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            collision.SendMessage("Eat_Shovel");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 10f);
    }
}
