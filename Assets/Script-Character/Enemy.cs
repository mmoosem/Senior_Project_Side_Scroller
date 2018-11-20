using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //player
    public Transform target;
    //how fast it moves
    public float speed=2;
    public int maxLife = 50;
    public int life;
    public int hurt;
    public bool seen=false;
    private bool searching = false;
    private float search = 0;
    public float direction=1f;
    private int playerLife;
    public int shotCount = 0;
    private bool hasShot = false;
    private float timer = 0;
    public GameObject EnemyBulletPre;
    private float bulletTimer;
    public Transform firePoint;
    private float damageCount;
    private float xPose;
    // Use this for initialization
    private GameObject myShot;
    public Vector2 initialPosition;
    private bool groundPounded=false;

    void Start () {
        //xPose = this.gameObject.GetComponent<Rigidbody2D>().transform.x;
        target = GameObject.Find("player_character").transform;
        hurt = GameObject.Find("player_character").GetComponent<playerController>().rangeDamage;
        initialPosition = transform.position;
        life = maxLife;
        playerLife = GameObject.Find("player_character").GetComponent<playerController>().health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target.position.x<this.gameObject.transform.position.x)
        {
            direction =-1f;
        }
        else
        {
            direction = 1f;
        }
        if (groundPounded == true)
        {
            damageCount += .01f;
        }
        if (damageCount >= 1f)
        {
            groundPounded = false;
        }
        if (hasShot == true ) { timer += .03f; }
        if(timer >= 5f) {
            hasShot = false;
            if (shotCount>=3) { shotCount = 0; }
            timer = 0;
            
        }
        playerLife = GameObject.Find("player_character").GetComponent<playerController>().health;
        if (GameObject.Find("player_character").GetComponent<playerController>().health <= 0)
        {
            /*transform.position = initialPosition;
            life = maxLife;
            seen = false;
            searching = false;
            search = 0;
           */
            Destroy(this.gameObject);
        }
        else { 
        if (life <= 0)
        {
                GameObject.Find("player_character").GetComponent<playerController>().points += 1;
                Destroy(this.gameObject);
        }
        float step = speed * Time.deltaTime;
            if (seen == false && searching == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);
            }
        if (seen == true)
        {
                if (hasShot==false) {

                    myShot =Instantiate(EnemyBulletPre, firePoint.position, firePoint.rotation);
                    myShot.GetComponent<Rigidbody2D>().velocity=new Vector2(direction*15f,0);
                    shotCount++;
                    hasShot = true;
                }
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            }
         if (searching == true)
         {
                search += .01f;

             if (search >= 10f)
                {
                 seen = false;
                 searching = false;
                 search = 0f;
                }
            }
        }
        
    }


private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            life -= hurt;
            
        }
        if (coll.gameObject.tag == "Player" && groundPounded == false)
        {
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(this.gameObject.GetComponent<Rigidbody2D>().velocity.x+(3*direction), this.gameObject.GetComponent<Rigidbody2D>().velocity.y+3);
            GameObject.Find("player_character").GetComponent<playerController>().hurt(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        
        if (col.gameObject.tag == "Player")
        {
            seen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "GroundPound")
        {
            groundPounded = false;
        }
        if (col.gameObject.tag == "Player")
        {
            searching = true;
        }
    }

}
