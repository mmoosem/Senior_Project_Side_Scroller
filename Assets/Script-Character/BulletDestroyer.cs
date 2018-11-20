using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float count = 0f;
    private int damage = 5;
    private bool pierce;
    private void Start()
    {
        pierce = GameObject.Find("player_character").GetComponent<playerController>().pierceShot;
  

        rb.velocity = new Vector3((speed* GameObject.Find("player_character").GetComponent<playerController>().direction), 0, 0);//transform. * speed;
    }
    private void Update()
    {
       
        if (count >= .7f) { Destroy(gameObject); }
        count += .01f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall"||collision.gameObject.tag =="Ground" )
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {   
            collision.GetComponent<Enemy>().life -= damage;
            if (pierce == false)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "Breakable")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
       