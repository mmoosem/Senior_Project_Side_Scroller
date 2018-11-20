using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed = 15f;
    public Rigidbody2D rb;
    private float count = 0f;
    private int damage = 5;
    private void Start()
    {
   
      //  rb.velocity = new Vector3((speed * (3*GameObject.Find("Enemy").GetComponent<Enemy>().direction)), 0, 0);//transform. * speed;
    }
    private void Update()
    {

        if (count >= .4f) { Destroy(gameObject); }
        count += .01f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
          //  GameObject.Find("Enemy").GetComponent<Enemy>().shotCount--;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<playerController>().hurt(damage);
           // GameObject.Find("Enemy").GetComponent<Enemy>().shotCount--;
            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "Breakable")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}