using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag =="Player" && GameObject.Find("player_character").GetComponent<playerController>().bossKey == true)
        {
            Destroy(this.gameObject);
        }

    }
}
