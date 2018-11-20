using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlastScript : MonoBehaviour {

    private GameObject Player;
    public int damage;
    public bool inSight;
    public bool fire=false;
    public float inCount;
    public float downCount;
    public GameObject beamImage;
    // Use this for initialization
	void Start () {
        Player = GameObject.Find("player_character");
        inSight = false;
        inCount = 0;
        downCount = 0;
        damage = 20;
        beamImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (inSight == true)
        {
            inCount += .03f;
        }
        else
        {
            inCount = 0;
        }
        if (inCount >= 10f)
        {
            fire = true;
            beamImage.SetActive(true);

        }
        if (fire==true) { Player.GetComponent<playerController>().hurt(damage); }
        if (fire==true&&inSight==false)
        {
            downCount += .1f;
            if (downCount >= 4f)
            {
           
                fire = false;
                beamImage.SetActive(false);
                downCount = 0;
            }
        }
        
        
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            inSight = true;
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            inSight = false;
        }
    }
}
