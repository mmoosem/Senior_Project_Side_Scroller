using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
	void Start () {
        player = GameObject.Find("player_character");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D coll)
    {
       if (coll.gameObject.tag == "Enemy" )
        {
            player.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 8));
            coll.gameObject.GetComponent<Enemy>().life -= 5;
        }
    }
}
