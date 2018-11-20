using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {
    public int maxLife = 200;
    public int life;
    // Prefab for beam that makes a shockwave attack
    public GameObject GroundWaveLazer;
    //Prefab for beam tha hits platforms off to the side if player doesn't move for too long
    public GameObject PlatformBeam;
    //Prefab for attack if player is in main line of sight
    public GameObject ForwardCannon;

    // Use this for initialization
    void Start () {
        life = maxLife;
	}
	
	// Update is called once per frame
	void Update () {
        if (life <= 0)
        {
            GameObject.Find("player_character").GetComponent<playerController>().bossKey = true;
            Destroy(this.gameObject);
        }	
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            life -= 10;
        }
        if (col.gameObject.tag == "GroundPound")
        {
            GameObject.Find("player_character").GetComponent<Rigidbody2D>().velocity = new Vector2(20,10);

            life -= 20;
        }
    }

}
