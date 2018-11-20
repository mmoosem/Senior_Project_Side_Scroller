using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public bool isDash, isDoubleJump, isPierce, isWallJump, hasGun;
    private string name;
    // Use this for initialization
	void Start () {
       hasGun = isDash = isDoubleJump = isPierce = isWallJump = false;
        name = this.gameObject.name;
    }

    // Update is called once per frame
    void Update () {
        if (name == "DashPower")
        {
            isDash = true;
        }
        else if (name == "DoubleJumpPower")
        {
            isDoubleJump = true;
        }
        else if (name == "PiercePower")
        {
            isPierce= true;
        }
        else if (name == "WallJumpPower")
        {
            isWallJump = true;
        }
        else if (name == "GunPower")
        {
            hasGun = true;
        }
        else
        {
            isDash = isDoubleJump = isPierce = isWallJump = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isDash == true)
            {
                GameObject.Find("player_character").GetComponent<playerController>().dashing=true;
            }
            else if (isWallJump == true)
            {
                GameObject.Find("player_character").GetComponent<playerController>().wallJumping = true;
            }
            else if (isDoubleJump == true)
            {
                GameObject.Find("player_character").GetComponent<playerController>().doubleJumping = true;
            }
            else if (isPierce == true)
            {
                GameObject.Find("player_character").GetComponent<playerController>().pierceShot = true;
            }
            else if (hasGun == true)
            {
                GameObject.Find("player_character").GetComponent<playerController>().hasGun = true;

            }
            Destroy(this.gameObject);
        }
    }
}
